using DataFaker.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataFaker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISessionService _sessionService;

        public AccountController(ISessionService sessionService) => _sessionService = sessionService;

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _sessionService.UnloadIdentity(HttpContext);

            return RedirectToAction("Login");
        }
    }
}
