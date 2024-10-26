using EventManagement.API.Base;
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
		public async Task<IActionResult> GetCategoryById([FromQuery] GetCategoryByIdQuery getCategoryByIdQuery)
		{
			var response = await Mediator.Send((getCategoryByIdQuery));
			return NewResult(response);
		}
	}
}
