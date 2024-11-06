using EventManagement.Core;
using EventManagement.Core.Middleware;
using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Infrustructure.Seeder;
using EventManagement.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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


// connect to sql
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});



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


var app = builder.Build();

#region Seed
using (var scope = app.Services.CreateScope())
{
	try
	{
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
		var categorieService = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
		await Task.WhenAll(
				 RoleSeeder.SeedAsync(roleManager),
				 UserSeeder.SeedAsync(userManager),
				 CategorySeeder.SeedAsync(categorieService)
			);
	
	}
	catch(Exception ex)
	{
		// log error
	}
	
}

	#endregion

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);
#endregion


#region Custom Middleware
// Middlewares
app.UseMiddleware<ErrorHandlerMiddleware>();
#endregion

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
