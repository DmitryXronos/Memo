using System.ComponentModel.DataAnnotations;

namespace Memo.Auth.RequestModels;

/// <summary>Модель, использующаяся для входа пользователя</summary>
public sealed class LoginRequestModel
{
    /// <summary>Email пользователя</summary>
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    /// <summary>Пароль</summary>
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}