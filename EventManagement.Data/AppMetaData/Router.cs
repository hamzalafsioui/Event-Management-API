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
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Paginated = Prefix + "/Paginated";

		}
		public static class EventRouting
		{
			private const string Prefix = Rule + "/Event";
			public const string List = Prefix + "/List";
			public const string GetById = Prefix;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix;
			public const string Paginated = Prefix + "/Paginated";

		}
	}
}
