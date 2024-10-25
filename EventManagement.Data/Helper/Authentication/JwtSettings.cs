namespace EventManagement.Data.Helper.Authentication
{
	public class JwtSettings
	{
		public string SigningKey { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string LifeTimeInMinutes { get; set; }

	}
}
