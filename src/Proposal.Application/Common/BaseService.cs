using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Proposal.Application.Validators;
using Proposal.Domain.SeedWork;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace Proposal.Application.Common;

[ExcludeFromCodeCoverage]
public abstract class BaseService(INotification notification)
{
    protected readonly INotification _notification = notification;
    public virtual ValidationResult ValidationResult { get; protected set; }


    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
    {
        return await action();            
    }

    protected virtual void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);

        if (!ValidationResult.IsValid)
        {
            foreach (var error in ValidationResult.Errors)
            {
                _notification.AddNotification(error.PropertyName, error.ErrorMessage, NotificationModel.ENotificationType.BadRequestError);
            }
            throw new ValidatorException();
        }
    }
}