using DataFaker.Domain.Contas;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DataFaker.Web.Services;

public interface ISessionService
{
    Task LoadIdentity(HttpContext httpContext, ISessionUser usuarioDaSessao);
    Task UnloadIdentity(HttpContext httpContext);
}

public sealed class SessionService : ISessionService
{
    public async Task LoadIdentity(HttpContext httpContext, ISessionUser usuarioDaSessao)
    {
        await UnloadIdentity(httpContext);

        var claims = new List<(string key, string value)>
            {
                (ClaimTypes.Name, usuarioDaSessao.Nome),
                (ClaimTypes.Email, usuarioDaSessao.Email),
            }.Where(q => !string.IsNullOrWhiteSpace(q.value))
            .Select(q => new Claim(q.key, q.value))
            .ToList();

        var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        httpContext.User = userPrincipal;
        await httpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal, new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = false,
        });
    }

    public async Task UnloadIdentity(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
}
