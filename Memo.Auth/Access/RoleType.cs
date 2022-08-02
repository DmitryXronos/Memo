namespace Memo.Auth.Access;

/// <summary>Типы ролей</summary>
public enum RoleType : byte
{
    /// <summary>Никто</summary>
    None = 0,
    /// <summary>Пользователь</summary>
    User = 1,
    /// <summary>Администратор</summary>
    Admin = 2
}