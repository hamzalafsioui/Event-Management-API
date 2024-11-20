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
		IRequestHandler<AddSpeakerCommand, Response<string>>
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
		#endregion

	}
}
