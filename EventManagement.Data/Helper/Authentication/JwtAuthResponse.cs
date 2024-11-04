namespace EventManagement.Data.Helper.Authentication
{
	public class JwtAuthResponse
	{
		public string AccessToken { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}
	public record RefreshToken(string UserName, string TokenString, DateTime? ExpireAt);
}
