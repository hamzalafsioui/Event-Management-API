using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EventManagement.Data.Helper.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // to convert between string and enum
    public enum UserRoleEnum
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,
        [EnumMember(Value = "Speaker")]

        Speaker = 2,
        [EnumMember(Value = "Attendee")]

        Attendee = 3 ,
        [EnumMember(Value = "User")]

        User = 4
    }
}
