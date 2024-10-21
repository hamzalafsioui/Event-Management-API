using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
		IRequestHandler<AddUserCommand, Response<string>>,
		IRequestHandler<EditUserCommand, Response<string>>,
		IRequestHandler<DeleteUserCommand, Response<string>>
	{

		#region Fields
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public UserCommandHandler(IUserService userService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._userService = userService;
			this._mapper = mapper;
			this._stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Function

		public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			// mapping between User and Request
			var userMapper = _mapper.Map<User>(request);
			// Adding
			var result = await _userService.AddAsync(userMapper);
			if (result == "Success")
				return Created("Added Successfully");
			else
				return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
		{
			// check is Id is exist
			var user = await _userService.GetByIdAsync(request.UserId);
			// return NotFound
			if (user == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.UserId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");

			// mapping between user and Request
			_mapper.Map(request, user);
			// Call update service
			var result = await _userService.EditAsync(user);

			// return response
			if (result == "Success")
				return Success($"Edit Successfully In Id {user.Id}");
			else
				return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			// check is Id is exist
			var user = await _userService.GetByIdAsync(request.userId);
			// return NotFound
			if (user == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {request.userId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");


			// Call Delete service
			var result = await _userService.DeleteAsync(user);

			// return response
			if (result == "Success")
				return Deleted<string>($"{_stringLocalizer[SharedResourcesKeys.UserId]} {user.Id} {_stringLocalizer[SharedResourcesKeys.Deleted]}");
			else
				return BadRequest<string>();
		}

		#endregion
	}
}
