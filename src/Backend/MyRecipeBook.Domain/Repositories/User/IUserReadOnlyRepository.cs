namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<Entities.User?> GetbyEmailAndPassword(string email, string password);
}