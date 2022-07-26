using Memo.Notes.Interfaces;

namespace Memo.Notes.Services;

/// <summary>
/// Информация о текущем о пользователе
/// </summary>
public sealed class CurrentUserInfoServices : ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    public Guid UserId { get; set; }
}