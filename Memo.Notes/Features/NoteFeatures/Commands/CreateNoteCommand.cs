using System.ComponentModel.DataAnnotations;
using MediatR;
using Memo.Notes.Models;

namespace Memo.Notes.Features.NoteFeatures.Commands;

/// <summary>Команда на создание новой заметки пользователя</summary>
public sealed class CreateNoteCommand : IRequest<Guid>
{
    /// <summary>Заголовок заметки</summary>
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>Текст заметки</summary>
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;
    
    /// <summary>Id юзера</summary>
    public Guid UserId { get; set; }
    
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly ApplicationContext _context;

        public CreateNoteCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Title = command.Title,
                Text = command.Text
            };

            await _context.Notes.AddAsync(note, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}