namespace EventManagement.Data.Helper
{
	public class RateLimiting
	{
		public int Limit { get; set; }
		public int TimeWindowInSeconds { get; set; }
	}
}
