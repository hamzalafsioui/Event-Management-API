using AutoMapper;
using EventManagement.API.Base;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.AppMetaData;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class UserController : AppControllerBase
	{
        
        #region  Functions
        [HttpGet(Router.UserRouting.List)]
		public async Task<IActionResult> GetUserList()
		{
			var response = await Mediator.Send(new GetUserListQuery());
			return NewResult(response);
		}

		[HttpGet(Router.UserRouting.GetById)]
		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetUserByIdQuery(id));
			return NewResult(response);
		}

		[HttpPost(Router.UserRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddUserCommand user)
		{
			var response = await Mediator.Send(user);
			return NewResult(response);
		}

		#endregion




	}
}
