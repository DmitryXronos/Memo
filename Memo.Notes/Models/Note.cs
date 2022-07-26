namespace Memo.Notes.Models;

/// <summary>Заметка</summary>
public sealed class Note
{
    /// <summary>Идентификатор</summary>
    public Guid Id { get; set; }

    /// <summary>Заголовок заметки</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Текст заметки</summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>Дата создания</summary>
    public DateTime Date { get; set; }
    
    /// <summary>Id юзера - автора заметки</summary>
    public Guid UserId { get; set; }
}