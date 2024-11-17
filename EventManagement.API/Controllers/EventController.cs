using EventManagement.API.Base;
using EventManagement.Core.Features.Comments.Commands.Models;
using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Data.AppMetaData;
using EventManagement.Data.Helper.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class EventController : AppControllerBase
	{
		#region  Functions
		[Authorize(Policy = "GetEvent")]
		[HttpGet(Router.EventRouting.List)]
		[SwaggerOperation(
			Summary = "Get a list of events",
			OperationId = "GetEventList",
			Description = "<h3>Details</h3><p>Retrieve a list of all events available in the system.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetEventList()
		{
			var response = await Mediator.Send(new GetEventListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetById)]
		[SwaggerOperation(
			Summary = "Get event by ID",
			OperationId = "GetEventById",
			Description = "<h3>Details</h3><p>Retrieve a specific event based on its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetEventById([FromQuery] GetEventByIdQuery getEventByIdQuery)
		{
			var response = await Mediator.Send(getEventByIdQuery);
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.Paginated)]
		[SwaggerOperation(
			Summary = "Get paginated events",
			OperationId = "GetPaginatedEvents",
			Description = "<h3>Details</h3><p>Retrieve a paginated list of events based on query parameters.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Paginated([FromQuery] GetEventPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}

		[Authorize(Policy = "CreateEvent")]
		[HttpPost(Router.EventRouting.Create)]
		[SwaggerOperation(
			Summary = "Create a new event",
			OperationId = "CreateEvent",
			Description = "<h3>Details</h3><p>Create a new event with the specified details.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Create([FromBody] AddEventCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPut(Router.EventRouting.Edit)]
		[SwaggerOperation(
			Summary = "Edit an existing event",
			OperationId = "EditEvent",
			Description = "<h3>Details</h3><p>Update the details of an existing event based on its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Edit([FromBody] EditEventCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.EventRouting.Delete)]
		[SwaggerOperation(
			Summary = "Delete an event",
			OperationId = "DeleteEvent",
			Description = "<h3>Details</h3><p>Delete an event by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteEventCommand(id));
			return NewResult(response);
		}
		[HttpPatch(Router.EventRouting.Cancel)]
		[SwaggerOperation(
			Summary = "Cancel an event",
			OperationId = "CancelEvent",
			Description = "<h3>Details</h3><p>Cancel a scheduled event by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> CancelEvent([FromRoute] int id)
		{
			var response = await Mediator.Send(new CancelEventCommand(id));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetAttendees)]
		[SwaggerOperation(
			Summary = "Get event attendees",
			OperationId = "GetAttendees",
			Description = "<h3>Details</h3><p>Retrieve a list of attendees for a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAttendees([FromRoute] int eventId)
		{
			var response = await Mediator.Send(new GetEventAttendeesQuery(eventId));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetEventsByCategoryId)]
		[SwaggerOperation(
			Summary = "Get events by category",
			OperationId = "GetEventsByCategory",
			Description = "<h3>Details</h3><p>Retrieve a list of events filtered by category.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetEventsByCategoryId(int categoryId)
		{
			var response = await Mediator.Send(new GetEventsListByCategoryIdQuery(categoryId));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.UpcomingOrPast)]
		[SwaggerOperation(
			Summary = "Get upcoming or past events",
			OperationId = "GetUpcomingOrPastEvents",
			Description = "<h3>Details</h3><p>Retrieve a list of upcoming or past events based on a date comparison.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetUpcomingOrPastEvents([FromQuery] DateTimeComparison comparison)
		{
			var response = await Mediator.Send(new GetUpcomingOrPastEventsListQuery(comparison));
			return NewResult(response);
		}

		[HttpPost(Router.EventRouting.AddComment)]
		[SwaggerOperation(
			Summary = "Add comment to an event",
			OperationId = "AddCommentToEvent",
			Description = "<h3>Details</h3><p>Add a comment to a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> AddCommentToEvent([FromRoute] int eventId, [FromBody] AddCommentCommand command)
		{
			command.eventId = eventId;
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetComments)]
		[SwaggerOperation(
			Summary = "Get comments for an event",
			OperationId = "GetComments",
			Description = "<h3>Details</h3><p>Retrieve comments made on a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetComments([FromRoute] int eventId)
		{

			var response = await Mediator.Send(new GetCommentsListByEventIdQuery(eventId));
			return NewResult(response);
		}
		[HttpGet(Router.EventRouting.GetCommentsCountForEvent)]
		[SwaggerOperation(
			Summary = "Get comment count for an event",
			OperationId = "GetCommentsCountForEvent",
			Description = "<h3>Details</h3><p>Retrieve the number of comments for a specific event.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetCommentsCountForEvent(int eventId)
		{
			var response = await Mediator.Send(new GetCommentsCountForEventByIdQuery(eventId));
			return NewResult(response);
		}

		#endregion
	}
}
