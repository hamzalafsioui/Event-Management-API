using EventManagement.Core.Resources;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		#region Fields
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion

		#region Constructors
		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResources> stringLocalizer)
		{
			this._validators = validators;
			this._stringLocalizer = stringLocalizer;
		}
		#endregion

		#region Actions
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				if (failures.Count != 0)
				{
					var message = failures.Select(x => _stringLocalizer[x.PropertyName] + " : " + _stringLocalizer[x.ErrorMessage]).FirstOrDefault(); // propertyName = Key (Ex: Email)
					throw new ValidationException(message);
				}
			}
			return await next();

		}
		#endregion


	}
}
