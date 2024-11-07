using EventManagement.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
	public record DeleteRoleCommand(int Id):IRequest<Response<string>>;
	
}
