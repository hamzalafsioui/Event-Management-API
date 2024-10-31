using System.Text.Json.Serialization;

namespace EventManagement.Data.Helper.Enums
{

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum DateTimeComparison
	{
		Upcoming,
		Past
	}
}
