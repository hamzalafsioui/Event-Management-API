namespace EventManagement.Data.Helper.Authentication
{
	public class JwtSettings
	{
		public string SigningKey { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string LifeTimeInMinutes { get; set; }
		public int RefreshTokenExpireDate { get; set; }
		public bool   EnableRevocation {  get; set; }
		public bool validateAudience {  get; set; }
		public bool validateIssuer {  get; set; }
		public bool validateLifetime {  get; set; }
		public bool validateIssuerSigningKey {  get; set; }

	}
}
