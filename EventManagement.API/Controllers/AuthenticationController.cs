using EventManagement.API.Base;
using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Features.Authentication.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.AuthenticationRouting.SignIn)]
		public async Task<IActionResult> Create([FromBody] SignInCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.AuthenticationRouting.ValidateToken)]
		public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
		{
			var response = await Mediator.Send(query);
			return NewResult(response);
		}
		[HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
		{
			var response = await Mediator.Send(query);
			return NewResult(response);
		}
		[HttpPost(Router.AuthenticationRouting.SendResetPassword)]
		public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
