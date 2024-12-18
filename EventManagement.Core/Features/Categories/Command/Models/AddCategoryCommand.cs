﻿using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Categories.Command.Models
{
	public record AddCategoryCommand(string Name, string Description) : IRequest<Response<string>>;

}
