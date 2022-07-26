using System.ComponentModel.DataAnnotations;

namespace Memo.Auth.RequestModels;

/// <summary>Модель, использующаяся для регистрации пользователя</summary>
public sealed class RegisterRequestModel
{
    /// <summary>Электронная почта</summary>
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    /// <summary>Пароль</summary>
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;

    /// <summary>Имя пользователя</summary>
    [StringLength(30, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}