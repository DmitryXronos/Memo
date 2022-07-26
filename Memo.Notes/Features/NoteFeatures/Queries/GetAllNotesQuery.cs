using MediatR;
using Memo.Notes.Interfaces;
using Memo.Notes.Models;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes.Features.NoteFeatures.Queries;

/// <summary>
/// Запрос на получение всех заметок пользователя
/// </summary>
public sealed class GetAllNotesQuery : IRequest<IEnumerable<Note>>
{
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IEnumerable<Note>>
    {
        private readonly ApplicationContext _context;
        private readonly ICurrentUserInfoService _info;

        public GetAllNotesQueryHandler(ApplicationContext context, ICurrentUserInfoService info)
        {
            _context = context;
            _info = info;
        }
    
        public async Task<IEnumerable<Note>> Handle(GetAllNotesQuery query, CancellationToken cancellationToken)
        {
            var noteList = await _context.Notes
                .Where(p => p.UserId == _info.UserId)
                .ToListAsync(cancellationToken);

            return noteList.AsReadOnly();
        }
    }
}