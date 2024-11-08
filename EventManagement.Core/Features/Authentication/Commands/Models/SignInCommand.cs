using EventManagement.Core.Bases;
using EventManagement.Data.Responses;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
    public record SignInCommand(string UserName, string Password) : IRequest<Response<JwtAuthResponse>>;

}
