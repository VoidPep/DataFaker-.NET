using DataFaker.Domain.Contas;
using System.Security.Claims;

namespace DataFaker.Web.Helpers;

public static class UserHelpers
{
    public static ISessionUser? Usuario(this HttpContext context)
    {
        var user = context.User;

        //var id = user.Claims.FirstOrDefault(c => c.Type == CustomClaimType.Id)?.Value.ToInt();

        //if (id == null)
        //    return null;

        var nome = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (email == null)
            return null;

        var usuario = new SessionUser
        {
            Email = email,
            Nome = nome,
        };
        
        return usuario;
    }

}