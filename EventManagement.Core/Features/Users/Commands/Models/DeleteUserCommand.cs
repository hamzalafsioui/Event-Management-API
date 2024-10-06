using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public class DeleteUserCommand : IRequest<Response<string>>
	{
		#region Fields
		public  int userId;
		#endregion
		#region Constructors
		public DeleteUserCommand(int id)
		{
			this.userId = id;
		}
		#endregion




	}
}
