﻿using EventManagement.Service.Abstracts;
using EventManagement.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Service
{
	public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IEventService, EventService>();
			services.AddTransient<IAttendeeService, AttendeeService>();
			services.AddTransient<ICommentService, CommentService>();
			services.AddTransient<ICategoryService, CategoryService>();
			return services;
		}
	}
}
