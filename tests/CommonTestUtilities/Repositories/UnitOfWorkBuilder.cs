using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class UnitOfWorkBuilder
{
    public static IUnitfOfWork Build()
    {
        var mock = new Mock<IUnitfOfWork>();

        return mock.Object;
    }
}