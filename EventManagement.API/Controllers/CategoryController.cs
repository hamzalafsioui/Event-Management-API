using EventManagement.API.Base;
using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Features.Categories.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	
	[ApiController]
	public class CategoryController : AppControllerBase
	{
		[HttpGet(Router.CategoryRouting.List)]
		[SwaggerOperation(
			Summary = "Get List of Categories",
			OperationId = "GetCategoryList",
			Description = "<h3>Details</h3><p>Retrieve a list of all categories available in the system.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[AllowAnonymous]
		public async Task<IActionResult> GetCategoryList()
		{
			var response = await Mediator.Send(new GetCategoryListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.CategoryRouting.GetById)]
		[SwaggerOperation(
			Summary = "Get Category by ID",
			OperationId = "GetCategoryById",
			Description = "<h3>Details</h3><p>Retrieve the details of a category by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[AllowAnonymous]
		public async Task<IActionResult> GetCategoryById([FromRoute] int id)
		{
			var response = await Mediator.Send((new GetCategoryByIdQuery(id)));
			return NewResult(response);
		}

		[HttpPost(Router.CategoryRouting.Create)]
		[SwaggerOperation(
			Summary = "Create a New Category",
			OperationId = "CreateCategory",
			Description = "<h3>Details</h3><p>Create a new category with the specified details.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromBody] AddCategoryCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpPut(Router.CategoryRouting.Edit)]
		[SwaggerOperation(
			Summary = "Edit Existing Category",
			OperationId = "EditCategory",
			Description = "<h3>Details</h3><p>Update the details of an existing category.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit([FromBody] EditCategoryCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpDelete(Router.CategoryRouting.Delete)]
		[SwaggerOperation(
			Summary = "Delete Category",
			OperationId = "DeleteCategory",
			Description = "<h3>Details</h3><p>Delete a category by its unique identifier.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteCategoryCommand(id));
			return NewResult(response);
		}
	}
}
