using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Defines the contract for authentication services, including token generation, email confirmation, password reset, and validation.
	/// </summary>
	public interface IAuthenticationService
	{
		/// <summary>
		/// Generates a JWT access token for the specified <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> object representing the user for whom the token is generated.</param>
		/// <returns>A <see cref="JwtAuthResponse"/> containing the access token.</returns>
		public Task<JwtAuthResponse> GetJWTTokenAsync(User user);

		/// <summary>
		/// Generates a new refresh token for the specified <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> object representing the user for whom the refresh token is generated.</param>
		/// <param name="jwtToken">The <see cref="JwtSecurityToken"/> representing the user's current JWT token.</param>
		/// <param name="expiredDate">The expiration date of the refresh token.</param>
		/// <param name="refreshToken">The refresh token string that needs to be validated or updated.</param>
		/// <returns>A <see cref="JwtAuthResponse"/> containing the new access token and refresh token.</returns>
		public Task<JwtAuthResponse> GetRefreshTokenAsync(User user, JwtSecurityToken jwtToken, DateTime? expiredDate, string refreshToken);

		/// <summary>
		/// Validates the specified <paramref name="accessToken"/> and checks its expiration status.
		/// </summary>
		/// <param name="accessToken">The JWT access token to be validated.</param>
		/// <returns>
		/// A <see cref="string"/> indicating the result of the validation:
		/// <list type="bullet">
		/// <item><description><c>"NotExpired"</c>: The token is valid and not expired.</description></item>
		/// <item><description><c>"InvalidToken"</c>: The token is invalid or could not be validated.</description></item>
		/// <item><description>Any other string: Represents the error message if the token validation fails.</description></item>
		/// </list>
		/// </returns>
		public string ValidateToken(string accessToken);

		/// <summary>
		/// Reads and parses the specified <paramref name="accessToken"/> into a <see cref="JwtSecurityToken"/>.
		/// </summary>
		/// <param name="accessToken">The JWT access token string to be parsed.</param>
		/// <returns>The parsed <see cref="JwtSecurityToken"/> object.</returns>
		public JwtSecurityToken ReadJwtToken(string accessToken);

		/// <summary>
		/// Validates the details of a JWT token and refresh token asynchronously, checking if the token is expired, and validating the refresh token.
		/// </summary>
		/// <param name="jwtToken">The JWT security token to be validated.</param>
		/// <param name="accessToken">The access token associated with the JWT token.</param>
		/// <param name="refreshToken">The refresh token associated with the access token.</param>
		/// <returns>
		/// A tuple containing:
		/// <list type="bullet">
		/// <item><description><c>userId</c>: The user ID if the validation is successful, or an error message if not.</description></item>
		/// <item><description><c>expiredDate</c>: The expiration date of the refresh token, or <c>null</c> if validation fails.</description></item>
		/// </list>
		/// <list type="bullet">
		/// <item><description><c>"AlgorithmIsWrong"</c>: The algorithm used in the token is not HMACSHA256.</description></item>
		/// <item><description><c>"TokenIsNotExpired"</c>: The token is still valid and not expired.</description></item>
		/// <item><description><c>"RefreshTokenIsNotFound"</c>: The refresh token could not be found in the system.</description></item>
		/// <item><description><c>"RefreshTokenIsExpired"</c>: The refresh token has expired.</description></item>
		/// </list>
		/// </returns>
		public Task<(string userId, DateTime? expiredDate)> ValidateDetailsAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken);

		/// <summary>
		/// Confirms the email address of a user based on the provided user ID and confirmation code asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the user whose email is being confirmed.</param>
		/// <param name="code">The email confirmation code sent to the user.</param>
		/// <returns>
		/// A <see langword="string"/> indicating the result of the email confirmation process.
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The email was successfully confirmed.</description></item>
		/// <item><description><c>"ErrorWhenConfirmEmail"</c>: There was an error while confirming the email.</description></item>
		/// </list>
		/// </returns>
		public Task<string> ConfirmEmailAsync(int userId, string code);

		/// <summary>
		/// Sends a password reset code to the user's email address and updates the user's record with the generated code asynchronously.
		/// </summary>
		/// <param name="email">The email address of the user requesting a password reset.</param>
		/// <returns>
		/// A <see langword="string"/> indicating the result of the password reset code sending process.
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The reset code was successfully generated, updated, and sent to the user's email.</description></item>
		/// <item><description><c>"ErrorInUpdatedUser"</c>: There was an error updating the user's record with the generated code.</description></item>
		/// <item><description><c>"FailedWhenSendingToEmail"</c>: The reset code could not be sent to the user's email.</description></item>
		/// <item><description><c>Other error messages</c>: Any unexpected errors encountered during the process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> SendResetPasswordCodeAsync(string email);

		/// <summary>
		/// Confirms the password reset process by verifying the provided reset code against the user's stored code.
		/// </summary>
		/// <param name="email">The email address of the user attempting to reset their password.</param>
		/// <param name="code">The password reset code entered by the user.</param>
		/// <returns>
		/// A <see langword="string"/> indicating the result of the confirmation process.
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The code matches the stored code, confirming the password reset process.</description></item>
		/// <item><description><c>"UserNotFound"</c>: The user with the provided email was not found.</description></item>
		/// <item><description><c>"CodeIsNotCorrect"</c>: The provided reset code does not match the stored code for the user.</description></item>
		/// <item><description><c>Other error messages</c>: Any unexpected errors encountered during the process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> ConfirmResetPasswordAsync(string email, string code);

		/// <summary>
		/// Resets the user's password by removing the old password and setting a new one.
		/// </summary>
		/// <param name="email">The email address of the user whose password is to be reset.</param>
		/// <param name="password">The new password to be set for the user.</param>
		/// <returns>
		/// A <see langword="string"/> indicating the result of the password reset process.
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The password was successfully reset.</description></item>
		/// <item><description><c>"UserNotFound"</c>: No user was found with the provided email.</description></item>
		/// <item><description><c>Other error messages</c>: Any unexpected errors encountered during the password reset process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> ResetPasswordAsync(string email, string password);

		/// <summary>
		/// Sends a confirmation email to the specified <paramref name="user"/> with a link to confirm their email address.
		/// </summary>
		/// <param name="user">The user to whom the confirmation email will be sent.</param>
		/// <returns>
		/// The task <see cref="string"/>  indicating the status of the operation:
		/// - <b>"Success"</b> if the email was sent successfully.
		/// - <b>"FailedWhenSendEmail"</b> if sending the email failed.
		/// </returns>
		public Task<string> SendConfirmEmailAsync(User user);

	}
}
