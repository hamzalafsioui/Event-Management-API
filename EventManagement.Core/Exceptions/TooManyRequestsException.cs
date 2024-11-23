namespace EventManagement.Core.Exceptions
{
	public class TooManyRequestsException : Exception
	{
		public TooManyRequestsException(string message = "Too many requests. Please try again later.")
			: base(message)
		{

		}

	}
}
