using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Xunit;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCas();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCas(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.GetMessage("EMAIL_ALREADY_REGISTERED")));
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCas();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.GetMessage("NAME_EMPTY")));
    }

    private RegisterUserUseCase CreateUseCas(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncriptor = PasswordEncriptorBuilder.Build();
        var unitfOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();

        if (string.IsNullOrEmpty(email) == false)
            userReadOnlyRepository.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(writeOnlyRepository, userReadOnlyRepository.Build(), mapper, passwordEncriptor, unitfOfWork);
    }
}