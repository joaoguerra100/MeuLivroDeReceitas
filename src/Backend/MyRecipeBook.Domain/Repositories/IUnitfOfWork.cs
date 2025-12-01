namespace MyRecipeBook.Domain.Repositories;

public interface IUnitfOfWork
{
    public Task Commit();
}
