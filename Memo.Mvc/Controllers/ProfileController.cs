using Memo.Mvc.Access;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Mvc.Controllers;

/// <summary>Контроллер профиль пользователя</summary>
public sealed class ProfileController : Controller
{
    [CheckToken]
    public IActionResult Index()
    {
        return Ok();
    }
}