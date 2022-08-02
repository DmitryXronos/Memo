using System.ComponentModel.DataAnnotations;
using MediatR;
using Memo.Notes.Services;
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
    
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Guid>
    {
        private readonly ApplicationContext _context;
        private readonly ICurrentUserInfoService _info;
        
        public UpdateNoteCommandHandler(ApplicationContext context, ICurrentUserInfoService info)
        {
            _context = context;
            _info = info;
        }

        public async Task<Guid> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .Where(p => p.Id == command.Id && p.UserId == _info.UserId)
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