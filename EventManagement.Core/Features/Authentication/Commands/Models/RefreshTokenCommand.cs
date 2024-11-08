using EventManagement.Core.Bases;
using EventManagement.Data.Responses;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
    public record RefreshTokenCommand(string AccessToken,string RefreshToken):IRequest<Response<JwtAuthResponse>>;
	
}
