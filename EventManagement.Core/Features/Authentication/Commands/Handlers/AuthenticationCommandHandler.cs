using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Responses;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Commands.Handlers
{
	public class AuthenticationCommandHandler : ResponseHandler,
		IRequestHandler<SignInCommand, Response<JwtAuthResponse>>,
		IRequestHandler<RefreshTokenCommand, Response<JwtAuthResponse>>
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

		public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			// cheking if user exist or not
			var user = await _userManager.FindByNameAsync(request.UserName);
			// return no user found
			if (user == null)
			{
				return BadRequest<JwtAuthResponse>($"{_stringLocalizer[SharedResourcesKeys.IncrorrectData]}");
			}
			// try to sign in
			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			// return Failed if UserName/Email - Password wrong
			if (!signInResult.Succeeded)
			{
				return BadRequest<JwtAuthResponse>($"{_stringLocalizer[SharedResourcesKeys.IncrorrectData]}");

			}
			// generate Token 
			var result = await _authenticationService.GetJWTTokenAsync(user);
			// return token

			return Success<JwtAuthResponse>(result);
		}

		public async Task<Response<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
			var ValidateResult = await _authenticationService.ValidateDetailsAsync(jwtToken, request.AccessToken, request.RefreshToken);
			switch (ValidateResult)
			{
				case ("AlgorithmIsWrong", null):
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
				case ("TokenIsNotExpired", null):
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
				case ("RefreshTokenIsNotFound", null):
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
				case ("RefreshTokenIsExpired", null):
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);

			};

			var user = await _userManager.FindByIdAsync(ValidateResult.userId);
			if (user == null)
				return NotFound<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.UserId] + " " + _stringLocalizer[SharedResourcesKeys.NotFound]);

			var result = await _authenticationService.GetRefreshTokenAsync(user, jwtToken, ValidateResult.expiredDate, request.RefreshToken);
			return Success(result);
		}
		#endregion

	}
}
