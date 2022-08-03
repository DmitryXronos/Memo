using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Commands;

/// <summary>Команда на удаление заметки пользователя по Id</summary>
public sealed class DeleteNoteByIdCommand : IRequest<Guid>
{
    /// <summary>Id заметки</summary>
    public Guid Id { get; set; }
    
    /// <summary>Id юзера</summary>
    public Guid UserId { get; set; }
    
    public class DeleteNoteByIdCommandHandler : IRequestHandler<DeleteNoteByIdCommand, Guid>
    {
        private readonly ApplicationContext _context;
        
        public DeleteNoteByIdCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteNoteByIdCommand command, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .Where(p => p.Id == command.Id && p.UserId == command.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (note == null)
                return default;
            
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}