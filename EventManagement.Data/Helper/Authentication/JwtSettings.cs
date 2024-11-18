namespace EventManagement.Data.Helper.Authentication
{
	public class JwtSettings
	{
		public string SigningKey { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string LifeTimeInMinutes { get; set; }
		public int RefreshTokenExpireDate { get; set; }
		public bool EnableRevocation { get; set; }
		public bool ValidateAudience { get; set; }
		public bool ValidateIssuer { get; set; }
		public bool ValidateLifetime { get; set; }
		public bool ValidateIssuerSigningKey { get; set; }

	}
}
