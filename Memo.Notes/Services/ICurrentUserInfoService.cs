namespace Memo.Notes.Services;

/// <summary>
/// Информация о текущем пользователе
/// </summary>
public interface ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    Guid UserId { get; set; }
}