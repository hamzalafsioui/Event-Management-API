using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Queries.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Responses;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Queries.Handlers
{
	public class ClaimsQueryHandler : ResponseHandler,
		IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		#endregion
		#region Constructors
		public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
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

		public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
		{
			// get user
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return NotFound<ManageUserClaimsResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			var result = await _authorizationService.ManageUserClaimsData(user);

			return Success<ManageUserClaimsResponse>(result);

		}


		#endregion


	}
}

