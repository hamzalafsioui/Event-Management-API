﻿using EventManagement.API.Base;
using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Features.Authentication.Queries.Models;
using EventManagement.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventManagement.API.Controllers
{
	[ApiController]
	[Authorize] // Ensure only authenticated users can access most actions
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.AuthenticationRouting.SignIn)]
		[SwaggerOperation(
			Summary = "Sign in with user credentials",
			OperationId = "SignIn",
			Description = "<h3>Details</h3><p>Authenticate the user with the provided credentials (email and password) and return a JWT token.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[AllowAnonymous] // Allow unauthenticated users to sign in
		public async Task<IActionResult> Create([FromBody] SignInCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}


		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
		[SwaggerOperation(
			Summary = "Refresh the authentication token",
			OperationId = "RefreshToken",
			Description = "<h3>Details</h3><p>Refresh the user's JWT token using the provided refresh token.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)] // Forbidden if refresh token is invalid
															   // Refresh Token: Requires authentication, but doesn't need specific role-based access
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}


		[HttpGet(Router.AuthenticationRouting.ValidateToken)]
		[SwaggerOperation(
			Summary = "Validate the authentication token",
			OperationId = "ValidateToken",
			Description = "<h3>Details</h3><p>Validate the provided JWT token and check if it is still valid.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)] // Forbidden if the token is invalid or expired
		public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
		{
			var response = await Mediator.Send(query);
			return NewResult(response);
		}

		[HttpPost(Router.AuthenticationRouting.SendConfirmEmail)]
		[SwaggerOperation(
			Summary = "Send confirm email",
			OperationId = "SendConfirmEmail",
			Description = "<h3>Details</h3><p>Send a confirm email to the user with a link to confirm their email.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[AllowAnonymous] // Allow unauthenticated users to request email confirmation
		public async Task<IActionResult> SendConfirmEmail([FromQuery] SendConfirmEmailCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}


		[HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
		[SwaggerOperation(
			Summary = "Confirm the user's email address",
			OperationId = "ConfirmEmail",
			Description = "<h3>Details</h3><p>Confirm the user's email address using the provided confirmation code.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[AllowAnonymous] // Allow unauthenticated users to request email confirmation
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
		{
			var response = await Mediator.Send(query);
			return NewResult(response);
		}


		[HttpPost(Router.AuthenticationRouting.SendResetPassword)]
		[SwaggerOperation(
			Summary = "Send reset password email",
			OperationId = "SendResetPassword",
			Description = "<h3>Details</h3><p>Send a password reset email to the user with a link to reset their password.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[AllowAnonymous] // Allow unauthenticated users to request password reset
		public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}


		[HttpGet(Router.AuthenticationRouting.ConfirmResetPassword)]
		[SwaggerOperation(
			Summary = "Confirm password reset",
			OperationId = "ConfirmResetPassword",
			Description = "<h3>Details</h3><p>Confirm the user's password reset using the provided reset token.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
		{
			var response = await Mediator.Send(query);
			return NewResult(response);
		}


		[HttpPost(Router.AuthenticationRouting.ResetPassword)]
		[SwaggerOperation(
			Summary = "Reset the user's password",
			OperationId = "ResetPassword",
			Description = "<h3>Details</h3><p>Reset the user's password using the provided reset token and new password.</p>"
		)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		// Reset Password: Requires authentication
		public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
