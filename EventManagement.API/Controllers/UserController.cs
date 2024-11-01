using EventManagement.API.Base;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
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
		[HttpGet(Router.UserRouting.Paginated)]
		public async Task<IActionResult> Paginated([FromQuery] GetUserPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[Authorize]
		[HttpGet(Router.UserRouting.GetById)]
		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetUserByIdQuery(id));
			return NewResult(response);
		}

		[HttpPost(Router.UserRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpPut(Router.UserRouting.Edit)]
		public async Task<IActionResult> Edit([FromForm] EditUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpDelete(Router.UserRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteUserCommand(id));
			return NewResult(response);
		}

		[HttpPut(Router.UserRouting.ChangePassword)]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.UserRouting.GetUserComments)]
		public async Task<IActionResult> GetUserComments([FromRoute] int userId)
		{
			var response = await Mediator.Send(new GetUserCommentsQuery(userId));
			return NewResult(response);
		}

		#endregion




	}
}
