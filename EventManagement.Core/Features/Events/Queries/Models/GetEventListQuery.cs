﻿using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public record GetEventListQuery : IRequest<Response<List<GetEventListResponse>>>;

}
