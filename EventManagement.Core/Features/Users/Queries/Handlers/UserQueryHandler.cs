using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Handlers
{
	public class UserQueryHandler : IRequestHandler<GetUserListQuery, List<User>>
	{
		#region Fields
		private readonly IUserService _userService;

		#endregion

		#region Constructors
		public UserQueryHandler(IUserService userService)
		{
			this._userService = userService;
		}
		#endregion

		#region Handle Functions
		public async Task<List<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			return await _userService.GetUsersListAsync();
		}
		#endregion

	}
}
