using EventManagement.API.Base;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class UserController : AppControllerBase
	{

		#region  Functions
		[HttpGet(Router.UserRouting.List)]
		[SwaggerOperation(
		   Summary = "List of Users",
		   OperationId = "GetUserList",
		   Description = "<h3>Details</h3><p>Retrieve a list of all users.</p>"
	   )]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetUserList()
		{
			var response = await Mediator.Send(new GetUserListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.UserRouting.Paginated)]
		[SwaggerOperation(
		Summary = "Get Paginated Users",
		OperationId = "GetUserPaginated",
		Description = "<h3>Details</h3><p>Retrieve a paginated list of users based on query parameters.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Paginated([FromQuery] GetUserPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}

		[HttpGet(Router.UserRouting.GetById)]
		[SwaggerOperation(
		Summary = "Get User by ID",
		OperationId = "GetUserById",
		Description = "<h3>Details</h3><p>Retrieve user details by their unique identifier.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetUserByIdQuery(id));
			return NewResult(response);
		}

		[HttpPost(Router.UserRouting.Create)]
		[AllowAnonymous]
		[SwaggerOperation(
		Summary = "Create User",
		OperationId = "CreateUser",
		Description = "<h3>Details</h3><p>Create a new user with the specified details.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromForm] AddUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpPut(Router.UserRouting.Edit)]
		[SwaggerOperation(
		Summary = "Edit User",
		OperationId = "EditUser",
		Description = "<h3>Details</h3><p>Update the details of an existing user.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> Edit([FromForm] EditUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpDelete(Router.UserRouting.Delete)]
		[SwaggerOperation(
		Summary = "Delete User",
		OperationId = "DeleteUser",
		Description = "<h3>Details</h3><p>Delete a user by their unique identifier.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteUserCommand(id));
			return NewResult(response);
		}

		[HttpPut(Router.UserRouting.ChangePassword)]
		[SwaggerOperation(
		Summary = "Change User Password",
		OperationId = "ChangeUserPassword",
		Description = "<h3>Details</h3><p>Change the password of an existing user.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.UserRouting.GetUserComments)]
		[SwaggerOperation(
		Summary = "Get User Comments",
		OperationId = "GetUserComments",
		Description = "<h3>Details</h3><p>Retrieve comments made by a specific user.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetUserComments([FromRoute] int userId)
		{
			var response = await Mediator.Send(new GetUserCommentsQuery(userId));
			return NewResult(response);
		}

		[HttpGet(Router.UserRouting.GetUserEventEngagementSummary)]
		[SwaggerOperation(
		Summary = "Get User Event Engagement Summary",
		OperationId = "GetUserEventEngagementSummary",
		Description = "<h3>Details</h3><p>Retrieve a summary of the user's engagement with events.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetUserEventEngagementSummary()
		{
			var response = await Mediator.Send(new GetUserEventEngagementSummaryQuery());
			return NewResult(response);
		}
		[HttpGet(Router.UserRouting.GetUserEventEngagementDetailsByUserId)]
		[SwaggerOperation(
		Summary = "Get User Event Engagement Details",
		OperationId = "GetUserEventEngagementDetailsByUserId",
		Description = "<h3>Details</h3><p>Retrieve detailed event engagement data for a specific user.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetUserEventEngagementDetailsByUserId([FromRoute] int userId)
		{
			var response = await Mediator.Send(new GetUserEventEngagementDetailsQuery(userId));
			return NewResult(response);
		}
		#endregion




	}
}
