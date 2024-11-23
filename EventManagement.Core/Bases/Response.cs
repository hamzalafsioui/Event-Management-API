using System.Net;

namespace EventManagement.Core.Bases
{
	public class Response<T>
	{
		public HttpStatusCode StatusCode { get; set; }
		public bool Succeded { get; set; }
		public string? Message { get; set; }
		public object? Meta { get; set; }
		public List<string>? Errors { get; set; }
		public T? Data { get; set; }

		public Response()
		{

		}
		public Response(T data, string message = null!)
		{
			Succeded = true; // because Response contains data
			Message = message;
			Data = data;
		}
		public Response(string message)
		{
			Succeded = false; // because Response doesn't contain data
			Message = message;
		}

		public Response(string message, bool succeeded)
		{
			Succeded = succeeded;
			Message = message;

		}

	}
}
