using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authentication.Queries.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Queries.Handlers
{
	public class AuthenticationQueryHandler : ResponseHandler,
		IRequestHandler<AuthorizeUserQuery, Response<string>>,
		IRequestHandler<ConfirmEmailQuery, Response<string>>,
		IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthenticationService _authenticationService;

		#endregion
		#region Constructors
		public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
				 IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			_authenticationService = authenticationService;
		}


		#endregion
		#region Handle Functions

		public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
		{
			var result = _authenticationService.ValidateToken(request.AccessToken);
			if (result == "NotExpired")
				return Success(result);

			if (result == "InvalidToken")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvalidToken]);
			return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);

		}

		public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
		{
			var confirmEmail = await _authenticationService.ConfirmEmailAsync(request.UserId, request.Code);

			return confirmEmail switch
			{
				"Success" => Success<string>(_stringLocalizer[SharedResourcesKeys.EmailConfirmed]),
				"ErrorWhenConfirmEmail" => BadRequest<string>(),
				_ => BadRequest<string>(),
			};
		}

		public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
		{
			var resetPasswordResult = await _authenticationService.ConfirmResetPasswordAsync(request.Email, request.Code);

			return resetPasswordResult switch
			{
				"Success" => Success<string>(_stringLocalizer[SharedResourcesKeys.OperationSucceed]),
				"CodeIsNotCorrect" => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvalidCode]),
				_ => BadRequest<string>(resetPasswordResult),
			};
		}
		#endregion

	}
}
