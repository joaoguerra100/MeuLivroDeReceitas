using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Dtos;

public record FilterRecipesDto
{
    public string? RecipeTitle_Ingredient { get; init; }
    public IList<CookingTime> CookingTimes { get; init; } = [];
    public IList<Difficulty> Difficulty { get; init; } = [];
    public IList<DishType> DishType { get; init; } = [];
}
