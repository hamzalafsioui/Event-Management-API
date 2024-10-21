using EventManagement.Data.Abstracts;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper;

namespace EventManagement.Data.Entities
{
    public class Comment:IHasCreatedAt,IHasUpdatedAt
	{
		public int CommentId { get; set; }
		public int EventId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public CommentStatusEnum Status { get; set; } = CommentStatusEnum.Active; 

		public DateTime CreatedAt { get; set; } 
		public DateTime UpdatedAt { get; set; } 

		public Event Event { get; set; }
		public User User { get; set; }

		// override ToString() for representation
		public override string ToString()
		{
			return $"CommentId: {CommentId}, UserId: {UserId}, Content: {Content}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
		}
	}



}
