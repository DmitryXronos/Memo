using Memo.Mvc.Access;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Mvc.Controllers;

/// <summary>Контроллер домашней страницы</summary>
public sealed class HomeController : Controller
{
    [CheckToken]
    public IActionResult Index()
    {
        return View();
    }
}