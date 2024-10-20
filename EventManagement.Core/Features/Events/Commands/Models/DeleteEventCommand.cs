using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public class DeleteEventCommand : IRequest<Response<string>>
	{
		#region Fields
		public int EventId { get; set; }
		#endregion
		#region Constructors
		public DeleteEventCommand(int eventId)
		{
			EventId = eventId;
		}
		#endregion

	}
}
