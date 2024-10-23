using EventManagement.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public record ChangeUserPasswordCommand(int Id,string CurrentPassword,string NewPassword,string ConfirmPassword):IRequest<Response<string>>;
}
