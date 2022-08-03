using MediatR;
using Memo.Notes.Features.NoteFeatures.Commands;
using Memo.Notes.Features.NoteFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Notes.Controllers;

/// <summary>Контроллер по работе с заметками</summary>
[Route("api/[controller]/[action]")]
public sealed class NoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoteController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>Получение всех заметок пользователя</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllNotesQuery()));
    
    /// <summary>Получение всех заметок пользователя из определенного интервала времени</summary>
    [HttpGet]
    public async Task<IActionResult> GetByDate(GetNotesByDateQuery query)
        => Ok(await _mediator.Send(query));

    /// <summary>Создание заметки</summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>Удаление заметки</summary>
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteNoteByIdCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>Обновление заметки</summary>
    [HttpPost]
    public async Task<IActionResult> Update(UpdateNoteCommand command)
        => Ok(await _mediator.Send(command));
}