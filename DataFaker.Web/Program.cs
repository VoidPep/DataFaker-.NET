using DataFaker.Web.Configurations;
using DataFaker.Domain;
using DataFaker.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.AddAuthenticationConfiguration();

    var services = builder.Services;

    var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

    services.AddDbContext<DataFakerContext>(options =>
    {
        options.ConfigureWarnings(warningsConfigurationBuilder => warningsConfigurationBuilder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        options.UseSqlite(connectionString);
    });

    services.AddTransient<IDataFakerContext, DataFakerContext>();
    services.AddDomainInjection();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}

app.Run();