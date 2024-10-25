using EventManagement.API.Base;
using EventManagement.Core.Features.Authentication.Commands.Models;
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
	}
}
