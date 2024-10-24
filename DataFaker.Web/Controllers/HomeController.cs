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
                var corpo = "Olá!\nSegue em anexo o arquivo com os dados gerados.\nAtenciosamente,\nPedro";

                var to = Environment.GetEnvironmentVariable("SMTP_TO") ?? "";

                _emailService.EnviarEmailAsync("", "DataFaker - Dados Gerados", corpo, anexo);
                return File(stream, anexo.ContentType, anexo.Nome);
            }
            catch (Exception e)
            {
                return File(stream, anexo.ContentType, anexo.Nome);
            }
        }
    }
}