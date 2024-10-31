using EventManagement.API.Base;
using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Data.AppMetaData;
using EventManagement.Data.Helper.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class EventController : AppControllerBase
	{
		#region  Functions
		[HttpGet(Router.EventRouting.List)]
		public async Task<IActionResult> GetEventList()
		{
			var response = await Mediator.Send(new GetEventListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetById)]
		public async Task<IActionResult> GetEventById([FromQuery] GetEventByIdQuery getEventByIdQuery)
		{
			var response = await Mediator.Send(getEventByIdQuery);
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.Paginated)]
		public async Task<IActionResult> Paginated([FromQuery] GetEventPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[HttpPost(Router.EventRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddEventCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPut(Router.EventRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] EditEventCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.EventRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteEventCommand(id));
			return NewResult(response);
		}
		[HttpPatch(Router.EventRouting.Cancel)]
		public async Task<IActionResult> CancelEvent([FromRoute] int id)
		{
			var response = await Mediator.Send(new CancelEventCommand(id));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetAttendees)]
		public async Task<IActionResult> GetAttendees([FromRoute] int eventId)
		{
			var response = await Mediator.Send(new GetEventAttendeesQuery(eventId));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetEventsByCategoryId)]
		public async Task<IActionResult> GetEventsByCategoryId(int categoryId)
		{
			var response = await Mediator.Send(new GetEventsListByCategoryIdQuery(categoryId));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.UpcomingOrPast)]
		public async Task<IActionResult> GetUpcomingOrPastEvents([FromQuery] DateTimeComparison comparison)
		{
			var response = await Mediator.Send(new GetUpcomingOrPastEventsListQuery(comparison));
			return NewResult(response);
		}

		#endregion
	}
}
