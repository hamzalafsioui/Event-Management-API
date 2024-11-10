using EventManagement.API.Base;
using EventManagement.Core.Features.Emails.Commands.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	[ApiController]
	public class EmailController : AppControllerBase
	{
		[HttpPost(Router.EmailRouting.SendEmail)]
		public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
