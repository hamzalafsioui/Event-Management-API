using EventManagement.API.Base;
using EventManagement.Core.Features.Emails.Commands.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	public class EmailController : AppControllerBase
	{
		[HttpPost(Router.EmailRouting.SendEmail)]
		[SwaggerOperation(
			Summary = "Send an email",
			OperationId = "SendEmail",
			Description = "<h3>Details</h3><p>Send an email using the specified command with all necessary details like recipient, subject, and body content.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
