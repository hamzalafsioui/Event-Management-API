using EventManagement.Core.Bases;
using EventManagement.Core.Features.Emails.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Emails.Commands.Handlers
{
	public class EmailCommandHandler : ResponseHandler,
		IRequestHandler<SendEmailCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEmailService _emailService;

		#endregion
		#region Constructors
		public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
			IEmailService emailService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			this._emailService = emailService;
		}
		#endregion
		#region Handle Functions

		#endregion


		public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
		{
			var response = await _emailService.SendEmailAsync(request.Email, request.Message, "Confirm Email");
			if (response == "Success")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.OperationSucceed]);
			else
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
		}
	}
}
