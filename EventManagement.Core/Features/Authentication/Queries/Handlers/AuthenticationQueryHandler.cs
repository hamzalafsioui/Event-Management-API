using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authentication.Queries.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Queries.Handlers
{
	public class AuthenticationQueryHandler : ResponseHandler,
		IRequestHandler<AuthorizeUserQuery, Response<string>>
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
			var result = await _authenticationService.ValidateToken(request.AccessToken);
			if(result=="NotExpired")
				return Success(result);

			return BadRequest<string>(result);

		}
		#endregion

	}
}
