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
	public class UserController : ControllerBase
	{
		#region Fields
		private readonly IMediator _mediator;

		#endregion

		#region Constructors
		public UserController(IMediator mediator)
		{
			this._mediator = mediator;
		}
		#endregion

		#region  Functions
		[HttpGet(Router.UserRouting.List)]
		public async Task<IActionResult> GetUserList()
		{
			var response = await _mediator.Send(new GetUserListQuery());
			return Ok(response);
		}

		[HttpGet(Router.UserRouting.GetById)]
		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var response = await _mediator.Send(new GetUserByIdQuery(id));
			return Ok(response);
		}

		[HttpPost(Router.UserRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddUserCommand user)
		{
			var response = await _mediator.Send(user);
			return Ok(response);
		}

		#endregion




	}
}
