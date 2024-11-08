using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Queries.Models;
using EventManagement.Core.Features.Authorization.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Data.DTOs.Roles;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Queries.Handlers
{
	public class RoleQueryHandler : ResponseHandler,
		IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
		IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
		IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>

	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		#endregion
		#region Constructors
		public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
			IAuthorizationService authorizationService,
			IMapper mapper,
			UserManager<User> userManager) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			_mapper = mapper;
			_userManager = userManager;
		}

		#endregion
		#region Handle Functions
		public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
		{
			var roles = await _authorizationService.GetRolesListAsync();
			var result = _mapper.Map<List<GetRolesListResponse>>(roles);

			return Success<List<GetRolesListResponse>>(result);
		}

		public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
		{
			var role = await _authorizationService.GetRoleByIdAsync(request.Id);
			if (role == null)
				return NotFound<GetRoleByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			var roleMapping = _mapper.Map<GetRoleByIdResponse>(role);

			return Success<GetRoleByIdResponse>(roleMapping);
		}

		public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
		{
			// get user
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return NotFound<ManageUserRolesResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			var result = await _authorizationService.ManageUserRolesDate(user);

			return Success<ManageUserRolesResponse>(result);

		}


		#endregion


	}
}
