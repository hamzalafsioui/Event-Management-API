using EventManagement.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Features.Categories.Command.Models
{
	public record AddCategoryCommand(string Name,string Description):IRequest<Response<string>>;
	
}
