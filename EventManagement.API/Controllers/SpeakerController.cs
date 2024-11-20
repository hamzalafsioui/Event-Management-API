using EventManagement.API.Base;
using EventManagement.Core.Features.Speakers.Commands.Models;
using EventManagement.Core.Features.Speakers.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Models;
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

		[HttpGet(Router.SpeakerRouting.List)]
		[SwaggerOperation(
		   Summary = "List of Speakers",
		   OperationId = "GetSpeakerList",
		   Description = "<h3>Details</h3><p>Retrieve a list of all speakers.</p>"
	   )]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[Authorize(Roles = "Admin,Attendee,User")]
		public async Task<IActionResult> GetSpeakerList()
		{
			var response = await Mediator.Send(new GetSpeakerListQuery());
			return NewResult(response);
		}


		[HttpGet(Router.SpeakerRouting.GetById)]
		[SwaggerOperation(
		Summary = "Get Speaker by ID",
		OperationId = "GetSpeakerById",
		Description = "<h3>Details</h3><p>Retrieve Speaker details by their unique identifier.</p>"
	)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetSpeakerById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetSpeakerByIdQuery(id));
			return NewResult(response);
		}


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
