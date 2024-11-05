﻿using EventManagement.API.Base;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	[ApiController]
	[Authorize]
	public class AuthorizationController : AppControllerBase
	{
		[HttpPost(Router.AuthorizationRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
