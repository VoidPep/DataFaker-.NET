using DataFaker.Web.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

namespace DataFaker.Web.Configurations;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISessionService, SessionService>();

        var keyPath = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys"));

        builder.Services
            .AddDataProtection()
            .PersistKeysToFileSystem(keyPath)
            .SetApplicationName(".DataFaker");

        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Cookie.Name = ".DataFaker";
                options.Cookie.IsEssential = true;
                options.LoginPath = "/Conta/Login";
                options.LogoutPath = "/Conta/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
            });
    }
}