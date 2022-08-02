using MediatR;
using Memo.Notes.Services;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Commands;

/// <summary>Команда на удаление заметки пользователя по Id</summary>
public sealed class DeleteNoteByIdCommand : IRequest<Guid>
{
    /// <summary>Id заметки</summary>
    public Guid Id { get; set; }
    
    public class DeleteNoteByIdCommandHandler : IRequestHandler<DeleteNoteByIdCommand, Guid>
    {
        private readonly ApplicationContext _context;
        private readonly ICurrentUserInfoService _info;
        
        public DeleteNoteByIdCommandHandler(ApplicationContext context, ICurrentUserInfoService info)
        {
            _context = context;
            _info = info;
        }

        public async Task<Guid> Handle(DeleteNoteByIdCommand command, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .Where(p => p.Id == command.Id && p.UserId == _info.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (note == null)
                return default;
            
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}