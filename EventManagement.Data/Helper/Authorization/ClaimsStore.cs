using System.Security.Claims;

namespace EventManagement.Data.Helper.Authorization
{
	public static class ClaimsStore
	{
		public static List<Claim> claims = new List<Claim>()
		{
			new Claim("Get Event","false"),
			new Claim("Create Event","false"),
			new Claim("Edit Event","false"),
			new Claim("Delete Event","false"),
		};
	}
}
