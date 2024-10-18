using EventManagement.API.Base;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class EventController : AppControllerBase
	{
		#region  Functions
		[HttpGet(Router.EventRouting.GetById)]
		public async Task<IActionResult> GetEventById([FromQuery] GetEventByIdQuery getEventByIdQuery)
		{
			var response = await Mediator.Send(getEventByIdQuery);
			return NewResult(response);
		}
		#endregion
	}
}
