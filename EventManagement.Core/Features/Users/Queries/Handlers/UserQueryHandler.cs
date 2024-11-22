using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Entities.SPs;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Queries.Handlers
{
	public class UserQueryHandler : ResponseHandler,
		IRequestHandler<GetUserListQuery, Response<List<GetUserListResponse>>>,
		IRequestHandler<GetUserByIdQuery, Response<GetSingleUserResponse>>,
		IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>,
		IRequestHandler<GetUserCommentsQuery, Response<List<GetUserCommentsResponse>>>,
		IRequestHandler<GetUserEventEngagementSummaryQuery, Response<List<GetUserEventEngagementSummaryResponse>>>,
		IRequestHandler<GetUserEventEngagementDetailsQuery, Response<GetUserEventEngagementDetailsResponse>>

	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICommentService _commentService;
		private readonly IUserService _userService;

		#endregion

		#region Constructors
		public UserQueryHandler(UserManager<User> userManager, IMapper mapper,
			IStringLocalizer<SharedResources> stringLocalizer,
			ICommentService commentService,
			IUserService userService) : base(stringLocalizer)
		{
			this._userManager = userManager;
			this._mapper = mapper;
			_stringLocalizer = stringLocalizer;
			this._commentService = commentService;
			_userService = userService;
		}
		#endregion

		#region Handle Functions
		public async Task<Response<List<GetUserListResponse>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			var studentList = await _userManager.Users.ToListAsync();
			var studentListMapping = _mapper.Map<List<GetUserListResponse>>(studentList);

			var result = Success(studentListMapping);
			result.Meta = new
			{
				count = studentListMapping.Count,
			};
			return result;
		}

		public async Task<Response<GetSingleUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			if (request.Id < 1)
				return BadRequest<GetSingleUserResponse>(_stringLocalizer[SharedResourcesKeys.InvalidId]);
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
			if (user == null)
			{
				return NotFound<GetSingleUserResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetSingleUserResponse>(user);
			return Success(result);
		}

		public async Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();
			var PaginatedList = await _mapper.ProjectTo<GetUserPaginatedListResponse>(users)
											 .ToPaginatedListAsync(request.PageNumber, request.PageSize);
			PaginatedList.Meta = new
			{
				Count = PaginatedList.Data.Count()
			};

			return PaginatedList;
		}

		public async Task<Response<List<GetUserCommentsResponse>>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
		{
			// invalid Id
			if (request.userId < 1)
				return BadRequest<List<GetUserCommentsResponse>>(_stringLocalizer[SharedResourcesKeys.InvalidId]);
			// checking is Exist
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.userId));
			if (user == null)
			{
				return NotFound<List<GetUserCommentsResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var commentsList = await _commentService.GetUserCommentsListByUserIdAsync(request.userId);
			// mapping
			var commentListMapping = _mapper.Map<List<GetUserCommentsResponse>>(commentsList);
			return Success(commentListMapping);
		}

		public async Task<Response<List<GetUserEventEngagementSummaryResponse>>> Handle(GetUserEventEngagementSummaryQuery request, CancellationToken cancellationToken)
		{
			// fetch data
			var viewResult = await _userService.GetViewUserEventEngagementSummaryAsync();
			// auto mapper to map result to response
			var resultMapping = _mapper.Map<List<GetUserEventEngagementSummaryResponse>>(viewResult);
			return Success(resultMapping);
		}

		public async Task<Response<GetUserEventEngagementDetailsResponse>> Handle(GetUserEventEngagementDetailsQuery request, CancellationToken cancellationToken)
		{
			var parameters = _mapper.Map<SP_GetUserEventEngagementDetailsParameters>(request);
			var sp_Result = await _userService.GetUserEventEngagementDetailsAsync(parameters);
			if (sp_Result == null)
			{
				return BadRequest<GetUserEventEngagementDetailsResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetUserEventEngagementDetailsResponse>(sp_Result);
			return Success(result);
		}

		#endregion

	}
}
