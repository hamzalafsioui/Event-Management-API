using AutoMapper;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Service.Abstracts;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Handlers
{
	public class UserQueryHandler : IRequestHandler<GetUserListQuery, List<GetUserListResponse>>
	{
		#region Fields
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		#endregion

		#region Constructors
		public UserQueryHandler(IUserService userService, IMapper mapper)
		{
			this._userService = userService;
			this._mapper = mapper;
		}
		#endregion

		#region Handle Functions
		public async Task<List<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			var studentList = await _userService.GetUsersListAsync();
			var studentListMapping = _mapper.Map<List<GetUserListResponse>>(studentList);

			return studentListMapping;
		}
		#endregion

	}
}
