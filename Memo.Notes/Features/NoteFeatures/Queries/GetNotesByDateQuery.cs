using MediatR;
using Memo.Notes.Models;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Queries;

/// <summary>Запрос на получение всех заметок пользователя, попадающих в определенный интервал времени</summary>
public sealed class GetNotesByDateQuery : IRequest<IEnumerable<Note>>
{
    /// <summary>Время, все записи от которого попадают в выборку</summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>Время, все записи до которого попадают в выборку</summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>Id юзера</summary>
    public Guid UserId { get; set; }
    
    public class GetNotesByDateQueryHandler : IRequestHandler<GetNotesByDateQuery, IEnumerable<Note>>
    {
        private readonly ApplicationContext _context;

        public GetNotesByDateQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<Note>> Handle(GetNotesByDateQuery query, CancellationToken cancellationToken)
        {
            var notes = await _context.Notes
                .AsNoTracking()
                .Where(p => p.UserId == query.UserId && p.Date >= query.StartTime && p.Date <= query.EndTime)
                .ToArrayAsync(cancellationToken);

            return notes;
        }
    }
}