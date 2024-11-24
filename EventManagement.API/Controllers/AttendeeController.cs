using EventManagement.API.Base;
using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Features.Attendees.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{

	[ApiController]
	public class AttendeeController : AppControllerBase
	{
		[HttpPost(Router.AttendeeRouting.Create)]
		[Authorize]
		[SwaggerOperation(
			Summary = "Join an event",
			OperationId = "JoinEvent",
			Description = "<h3>Details</h3><p>Register the user as an attendee for a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize]  // Any authenticated user can join an event
		public async Task<IActionResult> JoinEvent([FromBody] AddAttendeeCommand addAttendeeCommand)
		{
			var response = await Mediator.Send(addAttendeeCommand);
			return NewResult(response);
		}
		[HttpPut(Router.AttendeeRouting.Edit)]
		[Authorize(Roles = "Admin,Attendee")]
		[SwaggerOperation(
			Summary = "Edit attendee information",
			OperationId = "EditAttendee",
			Description = "<h3>Details</h3><p>Update attendee details for a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin,Attendee")]  // Admin or the attendee themselves can edit the attendee details
		public async Task<IActionResult> Edit([FromBody] EditAttendeeCommand editAttendeeCommand)
		{
			var response = await Mediator.Send(editAttendeeCommand);
			return NewResult(response);
		}
		[HttpDelete(Router.AttendeeRouting.Leave)]
		[Authorize]
		[SwaggerOperation(
			Summary = "Leave an event",
			OperationId = "LeaveEvent",
			Description = "<h3>Details</h3><p>Remove the attendee from the specified event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize]  // Any authenticated user can leave an event
		public async Task<IActionResult> LeaveEvent([FromRoute] int eventId, [FromRoute] int userId)
		{
			var response = await Mediator.Send(new LeaveEventCommand(eventId, userId));
			return NewResult(response);
		}
		[HttpPatch(Router.AttendeeRouting.ChangeStatus)]
		[Authorize(Roles = "Admin,Attendee")]
		[SwaggerOperation(
			Summary = "Change RSVP status",
			OperationId = "ChangeRSVPStatus",
			Description = "<h3>Details</h3><p>Update the RSVP status of the attendee for the event (e.g., attending, maybe, not attending,Cancel).</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin,Attendee")]  // Admin or the attendee can change their RSVP status
		public async Task<IActionResult> ChangeStatus([FromBody] ChangeRSVPStatusCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPatch(Router.AttendeeRouting.MarkAttendance)]
		[SwaggerOperation(
			Summary = "Mark event attendance",
			OperationId = "MarkAttendance",
			Description = "<h3>Details</h3><p>Record the attendance of the attendee for the specified event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin")]  // Only Admin can mark attendance
		public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.AttendeeRouting.AttendeeRegistered)]
		[SwaggerOperation(
			Summary = "Get registered events for a user",
			OperationId = "GetRegisteredEventsForUser",
			Description = "<h3>Details</h3><p>Retrieve the list of events a user has registered for as an attendee.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin,Attendee")]  // Admin or Attendee can view the events they are registered for
		public async Task<IActionResult> GetRegisteredEventsForUser([FromRoute] int userId)
		{
			var response = await Mediator.Send(new GetRegisteredEventsForUserQuery(userId));
			return NewResult(response);
		}
	}
}
