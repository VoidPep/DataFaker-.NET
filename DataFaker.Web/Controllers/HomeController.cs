using DataFaker.Domain.Gerador;
using Microsoft.AspNetCore.Mvc;

namespace DataFaker.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (Usuario == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Gerar([FromQuery] int quantidade, [FromServices] IExcelFakeDataGenerator generator)
        {
            quantidade = quantidade < 10 ? 10 : quantidade;
            quantidade = quantidade > 1000 ? 1000 : quantidade;

            var stream = generator.Generate(quantidade);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "dados_gerados.xlsx");
        }
    }
}
