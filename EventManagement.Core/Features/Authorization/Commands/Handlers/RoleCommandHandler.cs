using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Commands.Handlers
{
	public class RoleCommandHandler : ResponseHandler,
		IRequestHandler<AddRoleCommand, Response<string>>,
		IRequestHandler<EditRoleCommand, Response<string>>,
		IRequestHandler<DeleteRoleCommand, Response<string>>,
		IRequestHandler<UpdateUserRoleCommand, Response<string>>
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

		public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.EditRoleAsync(request.Id, request.Name);
			if (result == "NotFound")
				return NotFound<string>();
			else if (result == "Success")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
			else
				return BadRequest<string>(result);
		}

		public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.DeleteRoleAsync(request.Id);
			if (result == "NotFound")
				return NotFound<string>();
			else if (result == "Used")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
			else if (result == "Success")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
			else
				return BadRequest<string>(result);
		}

		public async Task<Response<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserRoles(request);
			return result switch
			{
				"UserNotFound" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]),
				"FailedToRemoveOldRoles" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]),
				"FailedToAddNewRoles" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]),
				"FailedToUpdateUserRoles" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]),
				"Success" => Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]),
				_ => NotFound<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]),

			};
		}
		#endregion



	}
}
