using MyRecipeBook.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncriptorBuilder
{
    public static PasswordEncriptor Build() => new PasswordEncriptor("abc1234");
}