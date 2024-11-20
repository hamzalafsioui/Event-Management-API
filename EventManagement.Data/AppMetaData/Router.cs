namespace EventManagement.Data.AppMetaData
{
	public static class Router
	{
		private const string SingleRoute = "/{id}";
		public const string root = "Api";
		public const string version = "V1";
		public const string Rule = root + "/" + version;

		public static class UserRouting
		{
			private const string Prefix = Rule + "/Users";
			public const string List = Prefix + "/List";
			public const string Paginated = Prefix + "/Paginated";
			public const string GetById = Prefix + SingleRoute;
			public const string GetUserComments = Prefix + "/{userId}/comments";
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
			public const string ChangePassword = Prefix + "/Change-Password";
			public const string GetUserEventEngagementSummary = Prefix + "/Get-Users-Events-Engagement-Summary";
			public const string GetUserEventEngagementDetailsByUserId = Prefix + "/Get-User-Event-Engagement-Details/{userId}";

		}
		public static class EventRouting
		{
			private const string Prefix = Rule + "/Events";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + "/Paginated/id";
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Paginated = Prefix + "/Paginated";
			public const string Delete = Prefix + SingleRoute;
			public const string Cancel = Prefix + "/{id}/cancel";
			public const string GetAttendees = Prefix + "/{eventId}/attendees";
			public const string UpdateStatus = Prefix + "/{id}/status";
			public const string GetEventsByCategoryId = Prefix + "/category/{categoryId}";
			public const string UpcomingOrPast = Prefix + "/upcomingOrPast";

			public const string AddComment = Prefix + "/{eventId}/Comment";
			public const string GetComments = Prefix + "/{eventId}/comments";
			public const string GetCommentsCountForEvent = Prefix + "/{eventId}/comments/count";




		}
		public static class AuthenticationRouting
		{
			private const string Prefix = Rule + "/Authentication";
			public const string SignIn = Prefix + "/SignIn";
			public const string RefreshToken = Prefix + "/Refresh-Token";
			public const string ValidateToken = Prefix + "/Validate-Token";
			public const string ConfirmEmail = Prefix + "/ConfirmEmail";
			public const string SendResetPassword = Prefix + "/SendResetPassword";
			public const string ConfirmResetPassword = Prefix + "/ConfirmResetPassword";
			public const string ResetPassword = Prefix + "/ResetPassword";
			public const string SendConfirmEmail = Prefix + "/Send-Confirm-Email";


		}
		public static class AuthorizationRouting
		{
			private const string Prefix = Rule + "/Authorization";
			private const string Roles = Prefix + "/Role";
			private const string Claims = Prefix + "/Claims";

			public const string CreateRole = Roles + "/Create";
			public const string EditRole = Roles + "/Edit";
			public const string DeleteRole = Roles + "/Delete" + SingleRoute;
			public const string RoleList = Roles + Roles + "/Role-List";
			public const string GetRoleById = Roles + Roles + SingleRoute;
			public const string ManageUserRolesById = Roles + "/Manage-User-Roles/{userId}";
			public const string UpdateUserRoles = Prefix + Roles + "/Update-User-Roles";

			public const string ManageUserClaimsById = Claims + "/Manage-User-Claims/{userId}";
			public const string UpdateUserClaims = Prefix + Claims + "/Update-User-Claims";

		}
		public static class CategoryRouting
		{
			private const string Prefix = Rule + "/Category";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
		}
		public static class SpeakerRouting
		{
			private const string Prefix = Rule + "/Speaker";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
		}
		public static class AttendeeRouting
		{
			private const string Prefix = Rule + "/Attendees";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/JoinEvent";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
			public const string Leave = Prefix + "/{eventId}/{userId}/leave";
			public const string ChangeStatus = Prefix + "/status";
			public const string MarkAttendance = Prefix + "/attendance";
			public const string AttendeeRegistered = Prefix + "/{userId}/events";
		}
		public static class CommentRouting
		{
			private const string Prefix = Rule + "/Comments";
			public const string GetById = Prefix + "/{commentId}";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;

		};
		public static class EmailRouting
		{
			private const string Prefix = Rule + "/Email";
			public const string SendEmail = Prefix + "/SendEmail";


		};


	}
}
