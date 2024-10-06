using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;

namespace EventManagement.Core.Features.Users.Commands.Handlers
{
	public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
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

		#endregion

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
	}
}
