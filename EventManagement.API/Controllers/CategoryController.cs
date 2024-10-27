using EventManagement.API.Base;
using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Features.Categories.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : AppControllerBase
	{
		[HttpGet(Router.CategoryRouting.List)]
		public async Task<IActionResult> GetCategoryList()
		{
			var response = await Mediator.Send(new GetCategoryListQuery());
			return NewResult(response);
		}
		[HttpGet(Router.CategoryRouting.GetById)]
		public async Task<IActionResult> GetCategoryById([FromRoute] int id)
		{
			var response = await Mediator.Send((new GetCategoryByIdQuery(id)));
			return NewResult(response);
		}
		[HttpPost(Router.CategoryRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddCategoryCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPut(Router.CategoryRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] EditCategoryCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.CategoryRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteCategoryCommand(id));
			return NewResult(response);
		}
	}
}
