using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Core.Resources;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion

		#region Constructors
		public UserQueryHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._userManager = userManager;
			this._mapper = mapper;
			_stringLocalizer = stringLocalizer;
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
			var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id.Equals(request.Id));
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
											 .ToPaginatedListAsync(request.PageNumber,request.PageSize);
			PaginatedList.Meta = new
			{
				Count = PaginatedList.Data.Count()
			};
			
			return PaginatedList;
		}

		#endregion

	}
}
