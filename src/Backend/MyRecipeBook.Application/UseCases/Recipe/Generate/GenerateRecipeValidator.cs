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
        RuleFor(r => r.Ingredients).Must(ingredients => ingredients.Count == ingredients.Select(c => c).Distinct().Count()).WithMessage(ResourceMessagesException.GetMessage("DUPLICATED_INGREDIENTS_IN_LIST"));

        RuleFor(request => request.Ingredients).ForEach(rule =>
        {
            rule.Custom((value, context) =>
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    context.AddFailure("Ingredient", ResourceMessagesException.GetMessage("INGREDIENT_EMPTY"));
                    return;
                }

                if(value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1)
                {
                    context.AddFailure("Ingredient", ResourceMessagesException.GetMessage("INGREDIENT_NOT_FOLLOWING_PATTERN"));
                    return;
                }
            });
        });
    }
}
