using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Categories.Command.Models
{
	public record DeleteCategoryCommand(int CategoryId) : IRequest<Response<string>>;

}
