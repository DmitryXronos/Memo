namespace Memo.Notes.Interfaces;

public interface IJwtService
{
    /// <summary>Проверяет достоверность jwt</summary>
    bool VerifyJwt(string jwt);

    /// <summary>Извлекает полезные данныео пользователе из jwt</summary>
    ICurrentUserInfoService? GetPayload(string jwt);
}