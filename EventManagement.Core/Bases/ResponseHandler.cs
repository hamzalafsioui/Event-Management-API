using EventManagement.Core.Resources;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer _stringLocalizer;

		public ResponseHandler(IStringLocalizer stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
		}
		public Response<T> Deleted<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeded = true,
				Message = message ?? _stringLocalizer[SharedResourcesKeys.Deleted]
			};
		}

		public Response<T> Success<T>(T entity, object meta = null!)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeded = true,
				Message = _stringLocalizer[SharedResourcesKeys.OperationSucceed],
				Meta = meta
			};
		}

		public Response<T> Unauthorized<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeded = true,
				Message = message ?? _stringLocalizer[SharedResourcesKeys.Unauthorized]
			};
		}

		public Response<T> BadRequest<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeded = false,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.BadRequest] : message
			};
		}
		public Response<T> NotFound<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeded = false,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message
			};
		}

		public Response<T> UnprocessableEntity<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeded = false,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.UnprocessableEntity] : message

			};
		}


		public Response<T> Created<T>(T entity=default!, object meta = null!)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeded = true,
				Meta = meta,
				Message = _stringLocalizer[SharedResourcesKeys.Created]
			};
		}
		public Response<T> TooManyRequests<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.TooManyRequests,
				Succeded = false,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.TooManyRequest] : message

			};
		}



	}
}
