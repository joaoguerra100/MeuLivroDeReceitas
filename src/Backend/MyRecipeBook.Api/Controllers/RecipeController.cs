using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Api.Attributes;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Api.Controllers;

[AuthenticatedUser]
public class RecipeController : MyRecipeBookBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterRecipeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterRecipeUseCase useCase, [FromBody] RequestRecipeJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
