using EventManagement.Core.Features.Comments.Queries.Responses;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Comments
{
	public partial class CommentProfile
	{
		private void GetCommentByIdQueryMapping()
		{
			CreateMap<Comment, GetCommentByIdResponse>()
				.ForMember(x => x.CommentText, opt => opt.MapFrom(x => x.Content))
				.ForMember(x => x.creator, opt => opt.MapFrom(x => x.User.UserName));

		}
	}
}
