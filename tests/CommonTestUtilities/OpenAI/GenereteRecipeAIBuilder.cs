using Moq;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenAI;

namespace CommonTestUtilities.OpenAI;

public class GenereteRecipeAIBuilder
{
    public static IGenerateRecipeAI Build(GenerateRecipeDto dto)
    {
        var mock = new Mock<IGenerateRecipeAI>();

        mock.Setup(s => s.Generate(It.IsAny<List<string>>())).ReturnsAsync(dto);

        return mock.Object;
    }
}
