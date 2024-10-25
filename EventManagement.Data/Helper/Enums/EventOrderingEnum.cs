using System.Text.Json.Serialization;

namespace EventManagement.Data.Helper.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventOrderingEnum
    {
        EventId = 0,
        Title = 1,
        Location = 2,
        StartTime = 3,
        EndTime = 4,
        CategoryName = 5,
        Creator = 6,
        CreatedAt = 7,
    }
}
