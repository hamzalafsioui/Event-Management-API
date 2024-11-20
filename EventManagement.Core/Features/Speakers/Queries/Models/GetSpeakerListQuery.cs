using EventManagement.Core.Bases;
using EventManagement.Core.Features.Speakers.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Speakers.Queries.Models
{
	public record GetSpeakerListQuery() : IRequest<Response<List<GetSpeakerListResponse>>>;


}
