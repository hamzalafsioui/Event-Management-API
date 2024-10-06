using EventManagement.Core.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace EventManagement.Core.Middleware
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			this._next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var responseModel = new Response<string>()
				{
					Succeded = false,
					Message = error?.Message!
				};
				switch (error)
				{
					case UnauthorizedAccessException exception:
						// custom application error
						//responseModel.Message = "Unauthorized Access Exception";
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.Unauthorized;
						response.StatusCode = (int)HttpStatusCode.Unauthorized;
						break;
					case ValidationException exception:
						// custom validation error
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
						response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
						break;
					case KeyNotFoundException exception:
						// not found error
						//responseModel.Message = "The Request Element Can't Be Found";
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.NotFound;
						response.StatusCode = (int)HttpStatusCode.NotFound;
						break;
					case DbUpdateException exception:
						// can't update error 
						responseModel.Message = exception.Message;
						responseModel.StatusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					case Exception exception:
						if(exception.GetType().ToString() == "ApiException")
						{
							responseModel.Message = exception.Message;
							responseModel.Message += exception.InnerException == null ? "" : "\n" + exception.InnerException.Message; ;

							responseModel.StatusCode = HttpStatusCode.BadRequest;
							response.StatusCode = (int)HttpStatusCode.BadRequest;
						}
						// ...

						responseModel.Message = exception.Message;
						responseModel.Message += exception.InnerException == null ? "" : "\n" + exception.InnerException.Message; ;
						break;
					default:
						// unhandled error
						responseModel.Message = "Please The Application Admin";
						responseModel.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}
				var result = JsonSerializer.Serialize(responseModel);
				await response.WriteAsync(result);
			}
		}
	}
}
