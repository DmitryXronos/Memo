namespace Memo.Mvc.Services;

/// <summary>Интерфейс сервиса по работе с jwt</summary>
public interface IJwtService
{
    /// <summary>Проверяет достоверность jwt</summary>
    bool VerifyJwt(string token, out ICurrentUserInfoService? info);
}