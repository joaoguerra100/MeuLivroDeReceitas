using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate;

public class GenerateRecipeUseCase : IGenerateRecipeUseCase
{
    public Task<ResponseGenerateRecipeJson> Execute(RequestGenerateRecipeJson request)
    {
        throw new NotImplementedException();
    }
}
