using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncriptor _passwordEncriptor;

    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncriptor passwordEncriptor)
    {
        _repository = repository;
        _passwordEncriptor = passwordEncriptor;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encripterPassword = _passwordEncriptor.Encrypt(request.Password);

        var user = await _repository.GetbyEmailAndPassword(request.Email, encripterPassword) ?? throw new InvalidLoginException();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name 
        };
    }
}
