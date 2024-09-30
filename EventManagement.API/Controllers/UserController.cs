using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		#region Fields
		private readonly IMediator _mediator;

		#endregion

		#region Constructors
		public UserController(IMediator mediator)
		{
			this._mediator = mediator;
		}
		#endregion

		#region  Functions
		[HttpGet("/User/List")]
		public async Task<IActionResult> GetUserList()
		{
			var response = await _mediator.Send(new GetUserListQuery());
			return Ok(response);
		}
		#endregion
	

		

    }
}
