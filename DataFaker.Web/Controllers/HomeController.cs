using DataFaker.Domain.Email;
using DataFaker.Domain.Gerador;
using Microsoft.AspNetCore.Mvc;

namespace DataFaker.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IExcelFakeDataGenerator _generator;
        private readonly IEmailService _emailService;

        public HomeController(IExcelFakeDataGenerator generator, IEmailService emailService)
        {
            _generator = generator;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            if (Usuario == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Gerar([FromQuery] int quantidade)
        {
            quantidade = quantidade < 10 ? 10 : quantidade;
            quantidade = quantidade > 1000 ? 1000 : quantidade;

            var stream = _generator.Generate(quantidade);

            var anexo = new Anexo
            {
                Stream = stream,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Nome = "dados_gerados.xlsx",
            };

            try
            {
                _emailService.EnviarEmailAsync("pedrohm1009@gmail.com", "DataFaker", "Corpo do Email", anexo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return File(stream, anexo.ContentType, anexo.Nome);
        }
    }
}