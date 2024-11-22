using EventManagement.Core.Features.Users.Queries.Responses;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		private void GetUserCommentsByUserIdQueryMapping()
		{
			CreateMap<Comment, GetUserCommentsResponse>()
				.ForMember(x => x.CommentText, opt => opt.MapFrom(x => x.Content))
				.ForMember(x => x.EventTitle, opt => opt.MapFrom(x => x.Event.Title));
		}
	}
}
