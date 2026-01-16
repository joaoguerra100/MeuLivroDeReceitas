using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using Sqids;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapepr(services, configuration);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
    }

    private static void AddAutoMapepr(IServiceCollection services, IConfiguration configuration)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            MinLength = 10,
            Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
        });

        services.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping(sqids));
        }).CreateMapper());
    }
}