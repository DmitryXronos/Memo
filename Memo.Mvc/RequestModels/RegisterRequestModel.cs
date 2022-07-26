using System.ComponentModel.DataAnnotations;

namespace Memo.Mvc.RequestModels;

/// <summary>Модель, использующаяся для регистрации пользователя</summary>
public sealed class RegisterRequestModel
{
    /// <summary>Email пользователя</summary>
    [EmailAddress(ErrorMessage = "Email has an incorrect format!")]
    [StringLength(50, ErrorMessage = "Maximum length of an email address in 50 characters!")]
    public string Email { get; set; } = string.Empty;

    /// <summary>Пароль</summary>
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must contain from 4 to 20 characters!")]
    public string Password { get; set; } = string.Empty;

    /// <summary>Имя пользователя</summary>
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Username must contain from 1 to 30 characters!")]
    public string Name { get; set; } = string.Empty;
}