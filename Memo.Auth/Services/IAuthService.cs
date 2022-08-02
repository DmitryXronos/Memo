using Memo.Auth.RequestModels;

namespace Memo.Auth.Services;

/// <summary>Интерфейс сервиса авторизации</summary>
public interface IAuthService
{
    /// <summary>Выполняет вход за пользователя и возвращает токен в случае успеха</summary>
    Task<string> LoginAsync(LoginRequestModel requestModel);

    /// <summary>Регистрирует пользователя и возвращает токен в случае успеха</summary>
    Task<string> RegisterAsync(RegisterRequestModel requestModel);

    /// <summary>Меняет пароль пользователя и возвращает true в случае успеха</summary>
    Task<bool> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
}