using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Data.Helper.Email;
using EventManagement.Infrustructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EventManagement.Infrustructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentity<User, Role>(opt =>
			{
				// Password settings.
				opt.Password.RequireDigit = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequiredLength = 3;
				opt.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				opt.Lockout.MaxFailedAccessAttempts = 5;
				opt.Lockout.AllowedForNewUsers = true;

				//  settings.
				opt.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				opt.User.RequireUniqueEmail = true;
				opt.SignIn.RequireConfirmedEmail = true;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			// Authentication : JwtOptions
			var JwtSettings = new JwtSettings();
			configuration.GetSection(nameof(JwtSettings)).Bind(JwtSettings);
			services.AddSingleton(JwtSettings);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = JwtSettings.Issuer,
						ValidAudience = JwtSettings.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SigningKey)),
						ClockSkew = TimeSpan.Zero // Eliminate clock skew for token expiration
					};
				});

			// swagger Gen 
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Managment", Version = "v1" });
				c.EnableAnnotations();

				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer hGgKiRcJh4362hSAQOs')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
			{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				}
			},
			Array.Empty<string>()
			}
		   });
			});

			services.AddAuthorization(option =>
			{
				option.AddPolicy("CeateEvent", policy =>
				{
					policy.RequireClaim("Create Event", "True");
				});
				option.AddPolicy("GetEvent", policy =>
				{
					policy.RequireClaim("Get Event", "True");
				});
			});

			// Email Sender: SmtpSettings
			var SmtpSettings = new SmtpSettings();
			configuration.GetSection(nameof(SmtpSettings)).Bind(SmtpSettings);
			services.AddSingleton(SmtpSettings);
			return services;

		}
	}
}
