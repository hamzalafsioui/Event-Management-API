using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Handlers
{
	public class UserCommandHandler : ResponseHandler,
		IRequestHandler<AddUserCommand, Response<string>>,
		IRequestHandler<EditUserCommand, Response<string>>,
		IRequestHandler<DeleteUserCommand, Response<string>>,
		IRequestHandler<ChangeUserPasswordCommand, Response<string>>
	{

		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IUserService _userService;
		private readonly IFileService _fileService;
		#endregion
		#region Constructors
		public UserCommandHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer,
			IUserService userService,
			IFileService fileService) : base(stringLocalizer)
		{
			this._userManager = userManager;
			this._mapper = mapper;
			this._stringLocalizer = stringLocalizer;
			_fileService = fileService;
			_userService = userService;
		}
		#endregion
		#region Handle Function

		public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			string? imageFileName = null;
			if (request.Image != null && request.Image.Length != 0)
			{
				imageFileName = await _fileService.UploadImageAsync("UserImages", request.Image, null);
			}

			// mapping
			var identityUser = _mapper.Map<User>(request);
			identityUser.Image = imageFileName;
			// create
			var CreateResult = await _userService.AddAsync(identityUser, request.Password);

			return CreateResult switch
			{
				"Success" => Created<string>(),
				"ErrorInCreateUser" => BadRequest<string>(),
				"ErrorInAddRole" => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]),
				"FailedWhenSendEmail" => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedWhenSendEmail]),
				"Failed" => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]),
				_ => BadRequest<string>(CreateResult)
			};


		}

		public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
		{
			// check is Id is exist
			var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());
			// return NotFound
			if (OldUser == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.Id} {_stringLocalizer[SharedResourcesKeys.NotFound]}");

			// handle Image
			string? imageFileName = null;
			if ((request.Image != null && request.Image.Length != 0) || OldUser.Image != null)
			{
				imageFileName = await _fileService.UploadImageAsync("UserImages", request.Image!, OldUser.Image);
			}

			// mapping between user and Request
			var newUser = _mapper.Map(request, OldUser);
			newUser.Image = imageFileName;
			// Call update service
			var result = await _userManager.UpdateAsync(newUser);

			// return response
			if (result.Succeeded)
				return Success($"Edit Successfully In Id {newUser.Id}");
			else
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.FailedToUpdate]}");
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			// check is Id is exist
			var user = await _userManager.FindByIdAsync(request.id.ToString());
			// return NotFound
			if (user == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.id} {_stringLocalizer[SharedResourcesKeys.NotFound]}");

			// Call Delete service (applying soft delete in SaveChangesAsync())
			var result = await _userManager.DeleteAsync(user);

			// return response
			if (result.Succeeded)
				return Deleted<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {user.Id} {_stringLocalizer[SharedResourcesKeys.Deleted]}");
			else
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.FailedToDelete]}");
		}

		public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{
			// get user
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			// checking 
			if (user == null)
				return NotFound<string>();
			// change password
			var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
			// return response
			if (result.Succeeded)
				return Success<string>($"{_stringLocalizer[SharedResourcesKeys.PasswordChanged]}");
			else
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.BadRequest]} : {result.Errors?.FirstOrDefault()?.Description}");

		}

		#endregion
	}
}
