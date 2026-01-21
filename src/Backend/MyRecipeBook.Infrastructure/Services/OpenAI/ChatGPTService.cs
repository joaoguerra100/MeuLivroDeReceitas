using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenAI;
using OpenAI.Chat;

namespace MyRecipeBook.Infrastructure.Services.OpenAI;

public class ChatGPTService : IGenerateRecipeAI
{
    private const string CHAT_MODEL = "gpt-4o";

    private readonly ChatClient _chatClient;

    public ChatGPTService(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<GenerateRecipeDto> Generate(IList<string> ingredients)
    {

        var response = await _chatClient.CompleteChatAsync(
                    string.Join(", ", ingredients)
                );

        

        /* return new GenerateRecipeDto
        {
            // ajuste para o nome real da sua propriedade
            Title = response.[0].Text
        }; */

        return null!;

    }
}
