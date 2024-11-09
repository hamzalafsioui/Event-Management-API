using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Commands.Handlers
{
	public class ClaimsCommandHandler : ResponseHandler,
		IRequestHandler<UpdateUserClaimsCommand, Response<string>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		#region Fields

		#endregion

		#region Constructors
		public ClaimsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
			IAuthorizationService authorizationService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			this._authorizationService = authorizationService;
		}
		#endregion

		#region Handle Functions
		public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserClaims(request);
			return result switch
			{
				"UserNotFound" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]),
				"FailedToRemoveOldClaims" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]),
				"FailedToAddNewClaims" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]),
				"FailedToUpdateUserClaims" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserClaims]),
				"Success" => Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]),
				_ => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserClaims]),

			};
		}
		#endregion



	}
}
