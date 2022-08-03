using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Commands;

/// <summary>Команда на обновление заметки пользователя</summary>
public sealed class UpdateNoteCommand : IRequest<Guid>
{
    /// <summary>Id заметки</summary>
    public Guid Id { get; set; }
    
    /// <summary>Заголовок заметки</summary>
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>Текст заметки</summary>
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;
    
    /// <summary>Id юзера</summary>
    public Guid UserId { get; set; }
    
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Guid>
    {
        private readonly ApplicationContext _context;

        public UpdateNoteCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .Where(p => p.Id == command.Id && p.UserId == command.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (note == null)
                return default;

            note.Title = command.Title;
            note.Text = command.Text;
            await _context.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}