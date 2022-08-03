using System.Text;
using System.Text.Json;
using Memo.Mvc.RequestModels;

namespace Memo.Mvc.Services;

/// <summary>Сервис по авторизации пользователей</summary>
public sealed class AuthService : IAuthService
{
    /// <summary>Выполняет вход пользователя через сервис авторизации</summary>
    public async Task<string> LoginAsync(LoginRequestModel requestModel)
    {
        // Шлем запрос к сервису авторизации
        using var httpClient = new HttpClient();
        var json = JsonSerializer.Serialize(requestModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient
            .PostAsync("http://memo_auth:80/api/Auth/Login", content);

        // Читаем токен из ответа
        var token = await response.Content.ReadAsStringAsync();

        return token;
    }

    /// <summary>Выполняет регистрацию пользователя через микросервис авторизации</summary>
    public async Task<string> RegisterAsync(RegisterRequestModel requestModel)
    {
        // Шлем запрос к сервису авторизации
        using var httpClient = new HttpClient();
        var json = JsonSerializer.Serialize(requestModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient
            .PostAsync("http://memo_auth:80/api/Auth/Register", content);

        // Читаем токен из ответа
        var token = await response.Content.ReadAsStringAsync();

        return token;
    }
}