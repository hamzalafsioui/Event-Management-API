using EventManagement.Core.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventManagement.Core
{
	public static class ModuleCoreDependencies
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
			// Configuration of Mediator
			services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			// Configuration of AutoMapper
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			// Get Validators
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			return services;
		}
	}
}
