using EventManagement.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManagement.API.Base
{
	public class AppControllerBase : ControllerBase
	{
		#region Fields
		private IMediator? _mediatorInstace = null!;
		protected IMediator Mediator => _mediatorInstace ?? HttpContext.RequestServices.GetService<IMediator>()!;

		#endregion

		#region Constructors

		#endregion
		#region Actions
		public ObjectResult NewResult<T>(Response<T> response)
		{
			return response.StatusCode switch
			{
				HttpStatusCode.OK => new OkObjectResult(response),
				HttpStatusCode.Created => new CreatedResult(string.Empty, response),
				HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
				HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
				HttpStatusCode.NotFound => new NotFoundObjectResult(response),
				HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
				HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(response),
				//HttpStatusCode.Forbidden => new ForbidResult(),
				HttpStatusCode.Conflict => new ConflictObjectResult(response),
				HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
				_ => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest }
			};


		}
		#endregion
	}
}
