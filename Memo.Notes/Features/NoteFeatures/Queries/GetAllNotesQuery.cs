using MediatR;
using Memo.Notes.Models;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Queries;

/// <summary>
/// Запрос на получение всех заметок пользователя
/// </summary>
public sealed class GetAllNotesQuery : IRequest<IEnumerable<Note>>
{
    /// <summary>Id юзера</summary>
    public Guid UserId { get; set; }
    
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IEnumerable<Note>>
    {
        private readonly ApplicationContext _context;

        public GetAllNotesQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<Note>> Handle(GetAllNotesQuery query, CancellationToken cancellationToken)
        {
            var notes = await _context.Notes
                .AsNoTracking()
                .Where(p => p.UserId == query.UserId)
                .ToArrayAsync(cancellationToken);

            return notes;
        }
    }
}