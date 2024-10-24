using DataFaker.Domain.Email;
using DataFaker.Domain.Gerador;
using Microsoft.Extensions.DependencyInjection;

namespace DataFaker.Domain;

public static class DomainInjectionModule
{
    public static void AddDomainInjection(this IServiceCollection service)
    {
        service.AddTransient<IExcelFakeDataGenerator, ExcelFakeDataGenerator>();
        service.AddTransient<IEmailService, EmailService>();
    }
}