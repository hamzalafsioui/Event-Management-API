using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Speakers.Queries.Models;
using EventManagement.Core.Features.Speakers.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Speakers.Queries.Handlers
{
	public class SpeakerQueryHandler : ResponseHandler,
		IRequestHandler<GetSpeakerListQuery, Response<List<GetSpeakerListResponse>>>,
		IRequestHandler<GetSpeakerByIdQuery, Response<GetSpeakerByIdQueryResponse>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ISpeakerService _speakerService;
		private readonly IMapper _mapper;
		#region Fields

		#endregion
		#region Constructors
		public SpeakerQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
			ISpeakerService speakerService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_speakerService = speakerService;
			_mapper = mapper;
		}

		#endregion
		#region Actions
		public async Task<Response<List<GetSpeakerListResponse>>> Handle(GetSpeakerListQuery request, CancellationToken cancellationToken)
		{
			// get speakers
			var speakers = await _speakerService.GetSpeakersListAsync();
			// mapping
			var speakersMapping = _mapper.Map<List<GetSpeakerListResponse>>(speakers);
			return Success(speakersMapping);
		}

		public async Task<Response<GetSpeakerByIdQueryResponse>> Handle(GetSpeakerByIdQuery request, CancellationToken cancellationToken)
		{
			if (request.speakerId < 0)
				return BadRequest<GetSpeakerByIdQueryResponse>(_stringLocalizer[SharedResourcesKeys.InvalidId]);
			var speaker = await _speakerService.GetByIdAsync(request.speakerId);
			if (speaker == null)
				return BadRequest<GetSpeakerByIdQueryResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			var speakerMapping = _mapper.Map<GetSpeakerByIdQueryResponse>(speaker);
			return Success(speakerMapping);

		}
		#endregion

	}
}
