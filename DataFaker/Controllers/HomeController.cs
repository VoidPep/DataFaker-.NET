using DataFaker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
    }
}
