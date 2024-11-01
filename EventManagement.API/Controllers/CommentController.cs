using EventManagement.API.Base;
using EventManagement.Core.Features.Comments.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{

	[ApiController]
	public class CommentController : AppControllerBase
	{
		[HttpGet(Router.CommentRouting.GetById)]
		public async Task<IActionResult> GetCommentById([FromRoute] GetCommentByIdQuery getCommentByIdQuery)
		{
			var response = await Mediator.Send(getCommentByIdQuery);
			return NewResult(response);
		}
	}
}
