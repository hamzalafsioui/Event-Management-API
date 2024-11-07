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


		}
		public static class AuthorizationRouting
		{
			private const string Prefix = Rule + "/Authorization";
			public const string CreateRole = Prefix + "/Role/Create";
			public const string EditRole = Prefix + "/Role/Edit";
		

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


	}
}
