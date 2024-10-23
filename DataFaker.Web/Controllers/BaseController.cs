using DataFaker.Domain.Contas;
using DataFaker.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataFaker.Web.Controllers;

public class BaseController : Controller
{
    protected ISessionUser? Usuario => HttpContext.Usuario();

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        ViewBag.Usuario = Usuario;

        var actionDescriptor = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor;
        var controllerName = actionDescriptor.ControllerName;
        var actionName = actionDescriptor.ActionName;

        ViewBag.Action = actionName;
        ViewBag.Controller = controllerName;

        base.OnActionExecuting(filterContext);
    }
}
