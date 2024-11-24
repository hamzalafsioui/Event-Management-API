using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Data.Helper.Email;
using EventManagement.Infrustructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace EventManagement.Infrustructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
		{

			// Register the configuration settings using strongly typed objects
			// Authentication : JwtOptions
			var JwtSettings = new JwtSettings();
			configuration.GetSection(nameof(JwtSettings)).Bind(JwtSettings);
			services.AddSingleton(JwtSettings);

			// Email Sender: SmtpSettings
			var SmtpSettings = new SmtpSettings();
			configuration.GetSection(nameof(SmtpSettings)).Bind(SmtpSettings);
			services.AddSingleton(SmtpSettings);

			// Rate Limiting:
			var RateLimiting = new RateLimiting();
			configuration.GetSection(nameof(RateLimiting)).Bind(RateLimiting);
			services.AddSingleton(RateLimiting);


			// Add Identity Services with the necessary configuration for user management
			services.AddIdentity<User, Role>(options =>
			{
				ConfigureIdentityOptions(options);
			})
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();

			// Add JWT Authentication Configuration
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					ConfigureJwtBearerOptions(options, configuration);
				});


			// Add Authorization Policies (can be moved to another file if it grows)
			services.AddAuthorization(options =>
			{
				ConfigureAuthorizationPolicies(options);
			});


			// Register Swagger
			services.AddSwaggerGen(c =>
			{
				ConfigureSwagger(c);
			});

			return services;

		}

		// Method to configure identity options for better separation
		private static void ConfigureIdentityOptions(IdentityOptions options)
		{
			// Password settings.
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireNonAlphanumeric = true;
			options.Password.RequireUppercase = true;
			options.Password.RequiredLength = 5;
			options.Password.RequiredUniqueChars = 1;

			// Lockout settings.
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.AllowedForNewUsers = true;

			//  settings.
			options.User.AllowedUserNameCharacters =
			"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
			options.User.RequireUniqueEmail = true;
			options.SignIn.RequireConfirmedEmail = true;
		}


		// Method to configure JWT bearer options
		private static void ConfigureJwtBearerOptions(JwtBearerOptions options, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = jwtSettings!.ValidateIssuer,
				ValidateAudience = jwtSettings.ValidateAudience,
				ValidateLifetime = jwtSettings.ValidateLifetime,
				ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
				ClockSkew = TimeSpan.Zero // Eliminate clock skew for token expiration
			};

		}


		// Method to configure authorization policies
		private static void ConfigureAuthorizationPolicies(AuthorizationOptions options)
		{
			// Policy for creating an event
			options.AddPolicy("CreateEvent", policy =>
			{
				policy.RequireClaim("Create Event", "true");
			});


			// Policy for editing an event
			options.AddPolicy("EditEvent", policy =>
			{
				policy.RequireClaim("Edit Event", "true");
			});

			// Policy for Cancel an action
			options.AddPolicy("CancelEvent", policy =>
			{
				policy.RequireClaim("Cancel Event", "true");
			});

			// Policy for deleting an event
			options.AddPolicy("DeleteEvent", policy =>
			{
				policy.RequireClaim("Delete Event", "true");
			});

		}

		// Swagger configuration can be refactored into a method to keep things clean
		private static void ConfigureSwagger(SwaggerGenOptions c)
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Event Management API",
				Version = "v1",
				Description = "API for managing events, attendees, and more.",
				Contact = new OpenApiContact
				{
					Name = "Hamza LAFSIOUI",
					Email = "hamza.lafsioui@gmail.com"
				}
			});

			c.EnableAnnotations();
			c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme (Example: \"Bearer {token}\")",
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
		}
	}
}
