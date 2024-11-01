using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Comments.Queries.Models;
using EventManagement.Core.Features.Comments.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Comments.Queries.Handlers
{
	public class CommentQueryHandler : ResponseHandler,
		IRequestHandler<GetCommentByIdQuery, Response<GetCommentByIdResponse>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;

		#region Fields

		#endregion
		#region Constructors
		public CommentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
			ICommentService commentService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			this._commentService = commentService;
			this._mapper = mapper;
		}

		#endregion
		#region Hnadle Functions
		public async Task<Response<GetCommentByIdResponse>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
		{
			// Inavlid Id
			if (request.commentId < 1)
				return BadRequest<GetCommentByIdResponse>(_stringLocalizer[SharedResourcesKeys.InvalidId]);

			// check is comment exist
			var comment = await _commentService.getCommentByIdAsync(request.commentId);
			if (comment == null)
			{
				return BadRequest<GetCommentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			// mapping
			var commentMappig = _mapper.Map<GetCommentByIdResponse>(comment);
			return Success(commentMappig);
		}
		#endregion

	}
}
