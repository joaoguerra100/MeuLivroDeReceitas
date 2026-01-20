using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate;

public class GenerateRecipeValidator : AbstractValidator<RequestGenerateRecipeJson>
{
    public GenerateRecipeValidator()
    {
        var maximun_number_ingredients = MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE;

        RuleFor(r => r.Ingredients.Count).InclusiveBetween(1, maximun_number_ingredients).WithMessage(ResourceMessagesException.GetMessage("INVALID_NUMBER_INGREDIENTS"));
        RuleFor(r => r.Ingredients).Must(ingredients => ingredients.Count == ingredients.Select(c => c).Distinct().Count()).WithMessage(ResourceMessagesException.GetMessage("BUPLICATED_INGREDIENTS_IN_LIST"));


    }
}
