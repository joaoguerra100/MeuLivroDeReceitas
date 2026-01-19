using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter;

public class FilterRecipeValidator : AbstractValidator<RequestFilterRecipeJson>
{
    public FilterRecipeValidator()
    {
        RuleForEach(r => r.CookingTimes).IsInEnum().WithMessage(ResourceMessagesException.GetMessage("COOKING_TIME_NOT_SUPPORTED"));
        RuleForEach(r => r.Difficulty).IsInEnum().WithMessage(ResourceMessagesException.GetMessage("DIFFICULTY_LEVEL_NOT_SUPPORTED"));
        RuleForEach(r => r.DishType).IsInEnum().WithMessage(ResourceMessagesException.GetMessage("DISH_TYPE_NOT_SUPPORTED"));
    }
}
