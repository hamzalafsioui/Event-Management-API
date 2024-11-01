using AutoMapper;

namespace EventManagement.Core.Mapping.Comments
{
	public partial class CommentProfile : Profile
	{
		public CommentProfile()
		{
			GetCommentByIdQueryMapping();
		}
	}
}
