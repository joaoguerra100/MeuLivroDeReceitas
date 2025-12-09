using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.GetMessage("NAME_EMPTY"));
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.GetMessage("EMAIL_EMPTY"));
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.GetMessage("PASSWORD_TOO_SHORT"));
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.GetMessage("EMAIL_INVALID"));
        });
    }
}