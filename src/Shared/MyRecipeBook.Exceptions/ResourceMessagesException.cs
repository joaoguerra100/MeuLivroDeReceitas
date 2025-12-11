using System.Globalization;
using System.Resources;

namespace MyRecipeBook.Exceptions
{
    public static class ResourceMessagesException
    {
        public static readonly ResourceManager ResourceManager =
            new ResourceManager("MyRecipeBook.Exceptions.ResourceMessagesException", typeof(ResourceMessagesException).Assembly);

        public static string GetMessage(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"Message not found for key: {key}";
        }
    }
}
