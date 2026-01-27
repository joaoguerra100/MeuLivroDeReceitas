
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Application.Extensions;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Image;

public class AddUpdateImageCoverUseCase : IAddUpdateImageCoverUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeUpdateOnlyRepository _repository;
    private readonly IUnitfOfWork _unitfOfWork;
    private readonly IBlobStorageService _blobStorageService;

    public AddUpdateImageCoverUseCase(ILoggedUser loggedUser, IRecipeUpdateOnlyRepository repository, IUnitfOfWork unitfOfWork, IBlobStorageService blobStorageService)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitfOfWork = unitfOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task Execute(long recipeId, IFormFile file)
    {
        var loggedUser = await _loggedUser.User();

        var recipe = await _repository.GetById(loggedUser, recipeId);

        if(recipe != null)
            throw new NotFoundException(ResourceMessagesException.GetMessage("RECIPE_NOT_FOUND"));

        var filesteam = file.OpenReadStream();

        (var isValidImage, var extension) = filesteam.ValidateAndGetImageExtension();

        if(isValidImage.IsFalse())
        {
            throw new ErrorOnValidationException([ResourceMessagesException.GetMessage("ONLY_IMAGES_ACCEPTED")]);
        }

        if(string.IsNullOrEmpty(recipe!.ImageIdentifier))
        {
            recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

            _repository.Update(recipe);

            await _unitfOfWork.Commit();
        }

        await _blobStorageService.Upload(loggedUser, filesteam, recipe.ImageIdentifier);
    }
}
