using Memo.Auth.Interfaces;
using Memo.Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memo.Auth.Attributes;

/// <summary>Проверяет токен, пришедший в заголовках запроса</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class CheckTokenAttribute : Attribute, IActionFilter
{
    /// <summary>Вызывается до выполнения действия</summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Ищем токен из заголовков запроса
        var token = (string)context.HttpContext.Request.Headers
            .Where(p => p.Key == "token")
            .Select(p => p.Value)
            .FirstOrDefault();
        if (token is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Валидируем токен
        var jwtService = context.HttpContext.RequestServices.GetRequiredService<JwtService>();
        if (!jwtService.VerifyJwt(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Извлекаем payload
        var payload = jwtService.GetPayload(token);
        if (payload is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Добавляем payload в DI
        var payloadService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserInfoService>();
        payloadService.UserId = payload.UserId;
        payloadService.Role = payload.Role;
    }

    /// <summary>Вызывается после выполнения действия</summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
