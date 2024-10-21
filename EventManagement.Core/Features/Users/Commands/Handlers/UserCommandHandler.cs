using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Handlers
{
	public class UserCommandHandler : ResponseHandler,
		IRequestHandler<AddUserCommand, Response<string>>
	//	,IRequestHandler<EditUserCommand, Response<string>>,
	//IRequestHandler<DeleteUserCommand, Response<string>>
	{

		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public UserCommandHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._userManager = userManager;
			this._mapper = mapper;
			this._stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Function

		public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			// if Email is Exist
			var userByEmail = await _userManager.FindByEmailAsync(request.Email);
			// email is exist
			if (userByEmail != null)
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.Email]} {_stringLocalizer[SharedResourcesKeys.AlreadyExist]}");
			// if username  is Exist
			var userByUserName = await _userManager.FindByNameAsync(request.UserName);
			// username is exist
			if (userByUserName != null)
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.Username]} {_stringLocalizer[SharedResourcesKeys.AlreadyExist]}");
			// mapping
			var identityUser = _mapper.Map<User>(request);
			// create
			var CreateResult = await _userManager.CreateAsync(identityUser, request.Password);
			// Failed
			if (!CreateResult.Succeeded)
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.FailedToAddUser]} : {CreateResult.Errors?.FirstOrDefault()?.Description}");


			// Message

			// success
			return Created($"{_stringLocalizer[SharedResourcesKeys.Created]}");
		}

		//public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
		//{
		//	// check is Id is exist
		//	var user = await _userService.GetByIdAsync(request.UserId);
		//	// return NotFound
		//	if (user == null)
		//		return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.UserId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");

		//	// mapping between user and Request
		//	_mapper.Map(request, user);
		//	// Call update service
		//	var result = await _userService.EditAsync(user);

		//	// return response
		//	if (result == "Success")
		//		return Success($"Edit Successfully In Id {user.Id}");
		//	else
		//		return BadRequest<string>();
		//}

		//public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		//{
		//	// check is Id is exist
		//	var user = await _userService.GetByIdAsync(request.userId);
		//	// return NotFound
		//	if (user == null)
		//		return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.userId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");


		//	// Call Delete service
		//	var result = await _userService.DeleteAsync(user);

		//	// return response
		//	if (result == "Success")
		//		return Deleted<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {user.Id} {_stringLocalizer[SharedResourcesKeys.Deleted]}");
		//	else
		//		return BadRequest<string>();
		//}

		#endregion
	}
}
