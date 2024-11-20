using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Speakers.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Speakers.Commands.Handlers
{
	public class SpeakerCommandHandler : ResponseHandler,
		IRequestHandler<AddSpeakerCommand, Response<string>>,
		IRequestHandler<EditSpeakerCommand, Response<string>>,
		IRequestHandler<DeleteSpeakerCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ISpeakerService _speakerService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		#endregion
		#region Constructors
		public SpeakerCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
			ISpeakerService speakerService,
			IMapper mapper,
			UserManager<User> userManager) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_speakerService = speakerService;
			_mapper = mapper;
			_userManager = userManager;
		}


		#endregion
		#region Actions
		public async Task<Response<string>> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
		{
			// Map the request to Speaker entity
			var speaker = _mapper.Map<Speaker>(request);

			// Add speaker
			var addSpeakerResult = await _speakerService.AddAsync(speaker);
			if (addSpeakerResult.IsSuccess)
				return Created<string>();

			return addSpeakerResult.ErrorMessage switch
			{
				"Failed to add the speaker." => BadRequest<string>(),
				"User not found." => BadRequest<string>(),
				"Failed to update the user's role." => BadRequest<string>(),
				"Failed to assign the Speaker role." => BadRequest<string>(),
				"An unexpected error occurred." => BadRequest<string>(),
				_ => BadRequest<string>()
			};
		}

		public async Task<Response<string>> Handle(EditSpeakerCommand request, CancellationToken cancellationToken)
		{
			// get speaker
			var speaker = await _speakerService.GetByIdAsync(request.SpeakerId);
			// update speaker Info
			speaker!.Bio = request.Bio;
			var result = await _speakerService.UpdateAsyc(speaker);
			// return Success
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}

		public async Task<Response<string>> Handle(DeleteSpeakerCommand request, CancellationToken cancellationToken)
		{
			// get speaker
			var speaker = await _speakerService.GetByIdAsync(request.SpeakerId);
			// not exist
			if (speaker == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			// delete Speaker
			var Result = await _speakerService.DeleteAsync(speaker!);
			if (Result.IsSuccess)
				return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);

			return Result.ErrorMessage switch
			{
				// we can write an Description In Each BadRequest<string>(here)
				"FailedToDeleteTheSpeaker" => BadRequest<string>(),
				"UserNotFound" => BadRequest<string>(),
				"FailedToUpdateTheUser'sRole" => BadRequest<string>(),
				"FailedToRemoveTheSpeakerRole" => BadRequest<string>(),
				"AnUnexpectedErrorOccurred" => BadRequest<string>(),
				_ => BadRequest<string>()
			};
		}
		#endregion

	}
}
