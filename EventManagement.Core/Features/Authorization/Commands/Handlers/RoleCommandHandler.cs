using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Commands.Handlers
{
	public class RoleCommandHandler : ResponseHandler,
		IRequestHandler<AddRoleCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;

		#endregion
		#region Constructors
		public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.AddRoleAsync(request.RoleName);
			if (result == "Success")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.Created]);

			return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
		}
		#endregion



	}
}
