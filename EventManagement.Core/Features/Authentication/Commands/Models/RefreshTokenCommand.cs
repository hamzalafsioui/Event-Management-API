using EventManagement.Core.Bases;
using EventManagement.Data.Helper.Authentication;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record RefreshTokenCommand(string AccessToken,string RefreshToken):IRequest<Response<JwtAuthResponse>>;
	
}
