using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Handlers
{
	public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUserListQuery, Response<List<GetUserListResponse>>>,
										IRequestHandler<GetUserByIdQuery,Response<GetSingleUserResponse>>
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
		public async Task<Response<List<GetUserListResponse>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			var studentList = await _userService.GetUsersListAsync();
			var studentListMapping = _mapper.Map<List<GetUserListResponse>>(studentList);

			return Success(studentListMapping);
		}

		public async Task<Response<GetSingleUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserByIdAsync(request.Id);
			if(user == null)
			{
				return NotFound<GetSingleUserResponse>($"User with Id {request.Id} Not Found");
			}
			var result =  _mapper.Map<GetSingleUserResponse>(user);
			return Success(result);
		}
		#endregion

	}
}
