using EventManagement.API.Base;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Features.Authorization.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	[ApiController]
	//[Authorize]
	public class AuthorizationController : AppControllerBase
	{
		#region Roles
		[HttpPost(Router.AuthorizationRouting.CreateRole)]
		public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.EditRole)]
		public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.AuthorizationRouting.DeleteRole)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteRoleCommand(id));
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.RoleList)]
		public async Task<IActionResult> GetRoleList()
		{
			var response = await Mediator.Send(new GetRolesListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.GetRoleById)]
		public async Task<IActionResult> GetRoleById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetRoleByIdQuery(id));
			return NewResult(response);
		}
		[HttpGet(Router.AuthorizationRouting.ManageUserRolesById)]
		public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserRolesQuery(userId));
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
		public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		#endregion

		#region Claims
		[HttpGet(Router.AuthorizationRouting.ManageUserClaimsById)]
		public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserClaimsQuery(userId));
			return NewResult(response);
		}
		[HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
		public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		#endregion
	}
}
