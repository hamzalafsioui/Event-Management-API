using FluentValidation;
using MediatR;

namespace EventManagement.Core.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		#region Fields
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		#endregion

		#region Constructors
		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			this._validators = validators;
		}
		#endregion

		#region Actions
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResults.SelectMany(r=>r.Errors).Where(f=>f!=null).ToList();

                if (failures.Count != 0)
				{
					var message = failures.Select(x=>x.PropertyName+": "+ x.ErrorMessage).FirstOrDefault(); // propertyName = Key (Ex: Email)
					throw new ValidationException(message);
				}
            }
			return await next();

		}
		#endregion


	}
}
