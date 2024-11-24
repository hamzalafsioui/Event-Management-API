using System.Security.Claims;

namespace EventManagement.Data.Helper.Authorization
{
	public static class ClaimsStore
	{
		public static List<Claim> claims = new List<Claim>()
		{
		  // Claims for event actions with initial "false" state, indicating no permissions
            new Claim("Create Event", "false"),
			new Claim("Edit Event", "false"),
			new Claim("Cancel Event", "false"),
			new Claim("Delete Event", "false")

		};
	}
}
