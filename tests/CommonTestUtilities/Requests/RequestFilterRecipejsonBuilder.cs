using Bogus;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestFilterRecipejsonBuilder
{
    public static RequestFilterRecipeJson Build()
    {
        return new Faker<RequestFilterRecipeJson>()
                .RuleFor(u => u.CookingTimes, faker => faker.Make(1, () => faker.PickRandom<CookingTime>()))
                .RuleFor(u => u.Difficulty, faker => faker.Make(1, () => faker.PickRandom<Difficulty>()))
                .RuleFor(u => u.DishType, faker => faker.Make(1, () => faker.PickRandom<DishType>()))
                .RuleFor(u => u.RecipeTitle_Ingredient, faker => faker.Lorem.Word());
                
    }
}
