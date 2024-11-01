using EventManagement.API.Base;
using EventManagement.Core.Features.Comments.Commands.Models;
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
		[HttpPut(Router.CommentRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] EditCommentCommand editCommentCommand)
		{
			var response = await Mediator.Send(editCommentCommand);
			return NewResult(response);
		}
		[HttpDelete(Router.CommentRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteCommentCommand(id));
			return NewResult(response);
		}
	}
}
