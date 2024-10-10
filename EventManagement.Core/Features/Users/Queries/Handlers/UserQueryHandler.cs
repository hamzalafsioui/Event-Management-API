using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Core.Resources;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace EventManagement.Core.Features.Users.Queries.Handlers
{
	public class UserQueryHandler : ResponseHandler,
		IRequestHandler<GetUserListQuery, Response<List<GetUserListResponse>>>,
		IRequestHandler<GetUserByIdQuery, Response<GetSingleUserResponse>>,
		IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>

	{
		#region Fields
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion

		#region Constructors
		public UserQueryHandler(IUserService userService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._userService = userService;
			this._mapper = mapper;
			_stringLocalizer = stringLocalizer;
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
			if (request.Id < 1)
				return BadRequest<GetSingleUserResponse>("Invalid Id");
			var user = await _userService.GetByIdWithIncludeAsync(request.Id);
			if (user == null)
			{
				return NotFound<GetSingleUserResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetSingleUserResponse>(user);
			return Success(result);
		}

		public async Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<User, GetUserPaginatedListResponse>> expression = e => new GetUserPaginatedListResponse(e.UserId, e.Username, e.FirstName, e.LastName, e.Email, e.Image, e.Role.ToString(), e.CreatedAt);

			var FilterQuery = _userService.FilterUserPaginatedQueryable(request.OrderBy, request.Search!);
			var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			return PaginatedList;
		}


		#endregion

	}
}
