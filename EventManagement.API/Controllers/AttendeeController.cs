using EventManagement.API.Base;
using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Features.Attendees.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{

	[ApiController]
	public class AttendeeController : AppControllerBase
	{
		[HttpPost(Router.AttendeeRouting.Create)]
		public async Task<IActionResult> JoinEvent([FromBody] AddAttendeeCommand addAttendeeCommand)
		{
			var response = await Mediator.Send(addAttendeeCommand);
			return NewResult(response);
		}
		[HttpPut(Router.AttendeeRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] EditAttendeeCommand editAttendeeCommand)
		{
			var response = await Mediator.Send(editAttendeeCommand);
			return NewResult(response);
		}
		[HttpDelete(Router.AttendeeRouting.Leave)]
		public async Task<IActionResult> LeaveEvent([FromRoute] int eventId, [FromRoute] int userId)
		{
			var response = await Mediator.Send(new LeaveEventCommand(eventId, userId));
			return NewResult(response);
		}
		[HttpPatch(Router.AttendeeRouting.ChangeStatus)]
		public async Task<IActionResult> ChangeStatus([FromBody] ChangeRSVPStatusCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPatch(Router.AttendeeRouting.MarkAttendance)]
		public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.AttendeeRouting.AttendeeRegistered)]
		public async Task<IActionResult> GetRegisteredEventsForUser([FromRoute] int userId)
		{
			var response = await Mediator.Send(new GetRegisteredEventsForUserQuery(userId));
			return NewResult(response);
		}
	}
}
