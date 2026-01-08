using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Api.Filtros;

namespace MyRecipeBook.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
