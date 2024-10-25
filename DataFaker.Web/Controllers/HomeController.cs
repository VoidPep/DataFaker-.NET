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

        public IActionResult Gerar([FromQuery] int? quantidade, [FromQuery] string? email)
        {
            quantidade = quantidade < 10 ? 10 : quantidade;
            quantidade = quantidade > 1000 ? 1000 : quantidade;

            var stream = _generator.Generate(quantidade ?? 10);

            var anexo = new Anexo
            {
                Stream = stream,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Nome = "dados_gerados.xlsx",
            };
            var corpo = "Olá!\nSegue em anexo o arquivo com os dados gerados.\nAtenciosamente,\nPedro";

            //Environment.GetEnvironmentVariable("SMTP_TO")
            var to = email == null || email == "" ? "" : email;

            if (to == null || to == "")
                return File(stream, anexo.ContentType, anexo.Nome);

            _emailService.EnviarEmailAsync("", "DataFaker - Dados Gerados", corpo, anexo);
            return File(stream, anexo.ContentType, anexo.Nome);
        }
    }
}