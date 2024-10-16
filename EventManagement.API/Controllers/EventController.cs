using EventManagement.API.Base;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Data.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class EventController : AppControllerBase
	{
		#region  Functions
		[HttpGet(Router.EventRouting.GetById)]
		public async Task<IActionResult> GetEventById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetEventByIdQuery(id));
			return NewResult(response);
		}
		#endregion
	}
}
