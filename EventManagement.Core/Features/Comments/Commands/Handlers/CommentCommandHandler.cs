using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Comments.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Comments.Commands.Handlers
{
	public class CommentCommandHandler : ResponseHandler,
		IRequestHandler<AddCommentCommand, Response<string>>
	{

		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;
		#endregion
		#region Constructors
		public CommentCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
			ICommentService commentService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_commentService = commentService;
			_mapper = mapper;
		}
		#endregion
		#region Handl Functions
		public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
		{
			// explicite mapping without using automapper
			var comment = new Comment
			{
				EventId = request.eventId,
				UserId = request.userId,
				Content = request.content,
			};
			// call service 
			var result = await _commentService.AddAsync(comment);
			// operation failed
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

			// success
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Created]);
		}
		#endregion


	}
}
