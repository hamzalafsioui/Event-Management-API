using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EventManagement.Data.Helper
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum UserOrderingEnum
	{
		UserId = 0,
		Username = 1,
		FirstName = 2,
		LastName = 3,
		Email = 4,
		Role = 5,
		CreatedAt = 6,
	}
}
