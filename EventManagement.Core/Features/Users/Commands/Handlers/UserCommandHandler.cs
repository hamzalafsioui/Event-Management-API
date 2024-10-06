using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;

namespace EventManagement.Core.Features.Users.Commands.Handlers
{
	public class UserCommandHandler : ResponseHandler,
		IRequestHandler<AddUserCommand, Response<string>>,
		IRequestHandler<EditUserCommand, Response<string>>
	{

		#region Fields
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		#endregion
		#region Constructors
		public UserCommandHandler(IUserService userService, IMapper mapper)
		{
			this._userService = userService;
			this._mapper = mapper;
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
			var user = _userService.GetUserByIdAsync(request.UserId);
			// return NotFound
			if (user == null)
				return NotFound<string>($"user with id {request.UserId} not exist");

			// mapping between user and Request
			var userMapper = _mapper.Map<User>(request);
			userMapper.CreatedAt = user.Result.CreatedAt;
			// Call update service
			var result = await _userService.EditAsync(userMapper);

			// return response
			if (result == "Success")
				return Success($"Edit Successfully In Id {userMapper.UserId}");
			else
				return BadRequest<string>();
		}

		#endregion
	}
}
