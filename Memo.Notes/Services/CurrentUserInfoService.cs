namespace Memo.Notes.Services;

/// <summary>Информация о текущем о пользователе</summary>
public sealed class CurrentUserInfoService : ICurrentUserInfoService
{
    /// <summary>Id пользователя</summary>
    public Guid UserId { get; set; }
}