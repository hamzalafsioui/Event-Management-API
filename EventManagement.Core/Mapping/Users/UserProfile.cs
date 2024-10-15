using AutoMapper;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile : Profile
	{
		public UserProfile()
		{
			GetUserListMapping();
			GetUserByIdMapping();
			AddUserCommandMapping();
			EditUserCommandMapping();
		}

		private int CalculateAge(DateTime dateOfBirth)
		{
			var today = DateTime.UtcNow;
			var age = today.Year - dateOfBirth.Year;
			if (dateOfBirth.Date > today.AddYears(-age))
			{
				age--;
			}
			return age;
		}
	}
}
