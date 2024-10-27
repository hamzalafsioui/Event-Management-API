using EventManagement.API.Base;
using EventManagement.Core.Features.Attendees.Command.Models;
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
	}
}
