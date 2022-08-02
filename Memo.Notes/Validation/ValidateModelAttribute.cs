using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memo.Notes.Validation;

/// <summary>Валидирует модель, пришедшую в запросе</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class ValidateModelAttribute : Attribute, IActionFilter
{
    /// <summary>Вызывается до выполнения действия</summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        context.Result = new BadRequestResult();
    }

    /// <summary>Вызывается после выполнения действия</summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}