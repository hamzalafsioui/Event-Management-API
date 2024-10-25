using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Commands.Handlers
{
	internal class AuthenticationCommandHandler : ResponseHandler,
		IRequestHandler<SignInCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthenticationService _authenticationService;

		#endregion
		#region Constructors
		public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager,
			SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			this._userManager = userManager;
			_signInManager = signInManager;
			_authenticationService = authenticationService;
		}
		#endregion
		#region Handle Functions

		public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			// cheking if user exist or not
			var user = await _userManager.FindByNameAsync(request.UserName);
			// return no user found
			if (user == null)
			{
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.IncrorrectData]}");
			}
			// try to sign in
			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			// return Failed if UserName/Email - Password wrong
			if (!signInResult.Succeeded)
			{
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.IncrorrectData]}");

			}
			// generate Token 
			var accessToken = await _authenticationService.GetJWTToken(user);
			// return token

			return Success(accessToken);
		}
		#endregion

	}
}
