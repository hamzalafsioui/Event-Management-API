using EventManagement.API.Base;
using EventManagement.Core.Features.Speakers.Commands.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	public class SpeakerController : AppControllerBase
	{
		#region Actions

		[HttpPost(Router.SpeakerRouting.Create)]
		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Create Speaker",
			OperationId = "CreateSpeaker",
			Description = "<h3>Details</h3><p>Create a new Speaker with the specified details.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromForm] AddSpeakerCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		#endregion

	}
}
