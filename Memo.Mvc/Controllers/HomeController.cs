using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Mvc.Controllers;

/// <summary>
/// Контроллер домашней страницы
/// </summary>
public sealed class HomeController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}