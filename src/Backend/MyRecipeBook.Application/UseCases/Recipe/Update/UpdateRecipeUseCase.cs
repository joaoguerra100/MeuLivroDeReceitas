using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Update;

public class UpdateRecipeUseCase : IUpdateRecipeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitfOfWork _unitfOfWork;
    private readonly IMapper _mapper;
    private readonly IRecipeUpdateOnlyRepository _repository;

    public UpdateRecipeUseCase(ILoggedUser loggedUser, IUnitfOfWork unitfOfWork, IMapper mapper, IRecipeUpdateOnlyRepository repository)
    {
        _loggedUser = loggedUser;
        _unitfOfWork = unitfOfWork;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task Execute(long recipeId, RequestRecipeJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var recipe = await _repository.GetById(loggedUser, recipeId);

        if(recipe == null)
            throw new NotFoundException(ResourceMessagesException.GetMessage("RECIPE_NOT_FOUND"));

        recipe.Ingredients.Clear();
        recipe.Instructions.Clear();
        recipe.DishTypes.Clear();

        _mapper.Map(request,recipe);

        var instruction = request.Instructions.OrderBy(i => i.Step).ToList();
        for (int index = 0; index < instruction.Count; index++)
        {
            instruction[index].Step = index + 1;
        }

        recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instruction);

        _repository.Update(recipe);

        await _unitfOfWork.Commit();
    }

    private static void Validate(RequestRecipeJson request)
    {
        var result = new RecipeValidator().Validate(request);

        if(result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }
}
