using System.ComponentModel.DataAnnotations;

namespace Memo.Mvc.RequestModels;

/// <summary>Модель, использующаяся для входа пользователя</summary>
public sealed class LoginRequestModel
{
    /// <summary>Email пользователя</summary>
    [EmailAddress(ErrorMessage = "Email has an incorrect format!")]
    [StringLength(50, ErrorMessage = "Maximum length of an email address in 50 characters!")]
    public string Email { get; set; } = string.Empty;

    /// <summary>Пароль</summary>
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must contain from 4 to 20 characters!")]
    public string Password { get; set; } = string.Empty;
}