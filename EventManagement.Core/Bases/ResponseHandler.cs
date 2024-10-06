namespace EventManagement.Core.Bases
{
	public class ResponseHandler
	{
		public ResponseHandler()
		{

		}
		public Response<T> Deleted<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeded = true,
				Message = message ?? "Deleted Successfully"
			};
		}

		public Response<T> Success<T>(T entity, object meta = null!)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeded = true,
				Message = "Operation Successfully",
				Meta = meta
			};
		}

		public Response<T> Unauthorized<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeded = true,
				Message = "Unauthorized"
			};
		}

		public Response<T> BadRequest<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeded = false,
				Message = message == null ? "Bad Request" : message
			};
		}
		public Response<T> NotFound<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeded = false,
				Message = message == null ? "Not Found" : message
			};
		}

		public Response<T> UnprocessableEntity<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeded = false,
				Message = message == null ? "Unprocessable Entity" : message

			};
		}


		public Response<T> Created<T>(T entity, object meta = null!)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeded = true,
				Meta = meta,
				Message = "Created"
			};
		}
		public Response<T> TooManyRequests<T>(string message = null!)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.TooManyRequests,
				Succeded = false,
				Message = message == null ? "Too Many Requests" : message

			};
		}



	}
}
