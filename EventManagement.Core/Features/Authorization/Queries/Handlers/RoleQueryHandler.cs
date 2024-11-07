using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Queries.Models;
using EventManagement.Core.Features.Authorization.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Queries.Handlers
{
	public class RoleQueryHandler : ResponseHandler,
		IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
		IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>

	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;

		#endregion
		#region Constructors
		public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
			IAuthorizationService authorizationService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			_mapper = mapper;
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


		#endregion


	}
}
