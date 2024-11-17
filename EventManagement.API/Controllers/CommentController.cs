using EventManagement.API.Base;
using EventManagement.Core.Features.Comments.Commands.Models;
using EventManagement.Core.Features.Comments.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{

	[ApiController]
	public class CommentController : AppControllerBase
	{
		[HttpGet(Router.CommentRouting.GetById)]
		[SwaggerOperation(
			Summary = "Get Comment by ID",
			OperationId = "GetCommentById",
			Description = "<h3>Details</h3><p>Retrieve a comment by it's unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetCommentById([FromRoute] GetCommentByIdQuery getCommentByIdQuery)
		{
			var response = await Mediator.Send(getCommentByIdQuery);
			return NewResult(response);
		}
		[HttpPut(Router.CommentRouting.Edit)]
		[SwaggerOperation(
			Summary = "Edit Comment",
			OperationId = "EditComment",
			Description = "<h3>Details</h3><p>Update the content of an existing comment by it's unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Edit([FromBody] EditCommentCommand editCommentCommand)
		{
			var response = await Mediator.Send(editCommentCommand);
			return NewResult(response);
		}
		[HttpDelete(Router.CommentRouting.Delete)]
		[SwaggerOperation(
			Summary = "Delete Comment",
			OperationId = "DeleteComment",
			Description = "<h3>Details</h3><p>Delete a comment by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteCommentCommand(id));
			return NewResult(response);
		}
	}
}
