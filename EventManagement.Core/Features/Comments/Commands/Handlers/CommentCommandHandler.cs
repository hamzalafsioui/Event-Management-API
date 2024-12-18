﻿using AutoMapper;
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
		IRequestHandler<AddCommentCommand, Response<string>>,
		IRequestHandler<EditCommentCommand, Response<string>>,
		IRequestHandler<DeleteCommentCommand, Response<string>>
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
			if (result == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

			// success
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Created]);
		}

		public async Task<Response<string>> Handle(EditCommentCommand request, CancellationToken cancellationToken)
		{
			var comment = await _commentService.GetCommentByIdAsync(request.commentId);

			// explicite mapping without using automapper
			comment.Content = request.content;
			// call service 
			var result = await _commentService.UpdateAsync(comment);
			// operation failed
			if (result == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

			// success
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}

		public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			// retrieve comment
			var comment = await _commentService.GetCommentByIdAsync(request.commentId);
			if (comment == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			// call delete service
			var result = await _commentService.DeleteAsync(comment);
			// operation failed
			if (!result)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

			// success
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);

		}
		#endregion


	}
}
