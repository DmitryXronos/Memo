namespace Memo.Notes.Interfaces;

/// <summary>
/// Информация о текущем пользователе
/// </summary>
public interface ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    public Guid UserId { get; set; }
}