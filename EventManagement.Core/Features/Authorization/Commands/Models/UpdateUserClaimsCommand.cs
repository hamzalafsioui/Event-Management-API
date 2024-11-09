using EventManagement.Core.Bases;
using EventManagement.Data.Requests;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
	public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
	{

	}


}
