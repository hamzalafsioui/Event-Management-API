using EventManagement.API.Base;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Features.Authorization.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	[Authorize]
	public class AuthorizationController : AppControllerBase
	{
		#region Roles
		[HttpPost(Router.AuthorizationRouting.CreateRole)]
		[SwaggerOperation(
			Summary = "Create a new Role",
			OperationId = "CreateRole",
			Description = "<h3>Details</h3><p>Create a new role with the specified details.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.EditRole)]
		[SwaggerOperation(
			Summary = "Edit an existing Role",
			OperationId = "EditRole",
			Description = "<h3>Details</h3><p>Edit the details of an existing role.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.AuthorizationRouting.DeleteRole)]
		[SwaggerOperation(
			Summary = "Delete a Role",
			OperationId = "DeleteRole",
			Description = "<h3>Details</h3><p>Delete a role by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteRoleCommand(id));
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.RoleList)]
		[SwaggerOperation(
			Summary = "Get List of Roles",
			OperationId = "GetRoleList",
			Description = "<h3>Details</h3><p>Retrieve a list of all roles available in the system.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetRoleList()
		{
			var response = await Mediator.Send(new GetRolesListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.GetRoleById)]
		[SwaggerOperation(
			Summary = "Get Role by ID",
			OperationId = "GetRoleById",
			Description = "<h3>Details</h3><p>Retrieve the details of a role by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetRoleById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetRoleByIdQuery(id));
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.ManageUserRolesById)]
		[SwaggerOperation(
			Summary = "Get User Roles by User ID",
			OperationId = "ManageUserRoles",
			Description = "<h3>Details</h3><p>Retrieve the roles associated with a specific user.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserRolesQuery(userId));
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
		[SwaggerOperation(
			Summary = "Update User Roles",
			OperationId = "UpdateUserRoles",
			Description = "<h3>Details</h3><p>Update the roles for a specific user.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		#endregion

		#region Claims
		[HttpGet(Router.AuthorizationRouting.ManageUserClaimsById)]
		[SwaggerOperation(
			Summary = "Get User Claims by User ID",
			OperationId = "ManageUserClaims",
			Description = "<h3>Details</h3><p>Retrieve the claims associated with a specific user.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserClaimsQuery(userId));
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
		[SwaggerOperation(
			Summary = "Update User Claims",
			OperationId = "UpdateUserClaims",
			Description = "<h3>Details</h3><p>Update the claims associated with a specific user.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		#endregion
	}
}
