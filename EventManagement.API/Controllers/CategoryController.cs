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
		public async Task<IActionResult> GetEventList()
		{
			var response = await Mediator.Send(new GetCategoryListQuery());
			return NewResult(response);
		}
	}
}
