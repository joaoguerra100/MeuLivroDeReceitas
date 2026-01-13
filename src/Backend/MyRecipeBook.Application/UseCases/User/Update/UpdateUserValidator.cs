using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.GetMessage("NAME_EMPTY"));
        RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.GetMessage("EMAIL_EMPTY"));

        When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesException.GetMessage("EMAIL_INVALID"));
        });
    }
}
