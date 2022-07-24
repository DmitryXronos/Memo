using Memo.Auth.Enums;

namespace Memo.Auth.Models;

/// <summary>Пользователь</summary>
public sealed class User
{
    /// <summary>Уникальный идентификатор</summary>
    public Guid Id { get; set; }
    
    /// <summary>Электронная почта</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Имя пользователя</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Хэш пароля</summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>Email подтвержден?</summary>
    public bool IsEmailConfirmed { get; set; }
    
    /// <summary>Роль</summary>
    public RoleType Role { get; set; }

    /// <summary>Дата регистрации</summary>
    public DateTime RegistrationDate { get; set; }
}