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
			private const string Prefix = Rule + "/User";
			public const string List = Prefix + "/List";
			public const string Paginated = Prefix + "/Paginated";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
			public const string ChangePassword = Prefix + "/Change-Password";

		}
		public static class EventRouting
		{
			private const string Prefix = Rule + "/Event";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + "/Paginated/id";
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Paginated = Prefix + "/Paginated";
			public const string Delete = Prefix + SingleRoute;


		}
		public static class AuthenticationRouting
		{
			private const string Prefix = Rule + "/Authentication";
			public const string SignIn = Prefix + "/SignIn";


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
			private const string Prefix = Rule + "/Attendee";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Delete = Prefix + SingleRoute;
		}
	}
}
