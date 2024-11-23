using EventManagement.Core;
using EventManagement.Core.Filters;
using EventManagement.Core.Middleware;
using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Infrustructure.Seeder;
using EventManagement.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region Json String Enum Converter Globally
// Add JsonStringEnumConverter Globally
//builder.Services.AddControllers()
//	.AddJsonOptions(options =>
//	{
//		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//	});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Connect to Sql
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});
#endregion


#region Dependency Injections

builder.Services.AddInfrustructureDependencies()
				.AddServiceDependencies()
				.AddCoreDependencies()
				.AddServiceRegistration(builder.Configuration);

#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
	opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	List<CultureInfo> supportedCultures = new List<CultureInfo>
	{
		new CultureInfo("en-US"),
		new CultureInfo("ar-AR"),
		new CultureInfo("fr-FR")
	};
	options.DefaultRequestCulture = new RequestCulture("en-US");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

#endregion

#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(opt =>
{
	opt.AddPolicy(name: MyAllowSpecificOrigins,
		policy =>
		{
			policy.AllowAnyHeader();
			policy.AllowAnyOrigin();
			policy.AllowAnyMethod();
		});
});
#endregion

#region IUrlHelper
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
	var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
	var factory = x.GetRequiredService<IUrlHelperFactory>();
	return factory.GetUrlHelper(actionContext!);
});
#endregion

#region Filter
builder.Services.AddTransient<AuthFilter>();
#endregion

#region Serilog
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();
#endregion
var app = builder.Build();

#region Seed
using (var scope = app.Services.CreateScope())
{
	try
	{
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
		var categorieService = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();

		await RoleSeeder.SeedAsync(roleManager);
		await UserSeeder.SeedAsync(userManager);
		await CategorySeeder.SeedAsync(categorieService);

	}
	catch (Exception ex)
	{
		Log.Error(ex, "An error occurred during database seeding");
	}

}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management API v1");
	});

}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);
#endregion


#region Custom Middleware
// Middlewares
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();

#endregion

app.UseHttpsRedirection();
app.UseCors();

app.UseStaticFiles();

app.UseAuthentication();
#region Custom Middleware
app.UseMiddleware<UpdateLastLoginMiddleware>();
#endregion
app.UseAuthorization();




app.MapControllers();

app.Run();
