using MediatR;
using Memo.Notes.Attributes;
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
    [CheckToken]
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllNotesQuery()));
    
    /// <summary>Получение всех заметок пользователя из определенного интервала времени</summary>
    [CheckToken]
    [HttpGet]
    public async Task<IActionResult> GetById(GetNotesByDateQuery query)
        => Ok(await _mediator.Send(query));

    /// <summary>Создание заметки</summary>
    [ValidateModel]
    [CheckToken]
    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>Удаление заметки</summary>
    [CheckToken]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteNoteByIdCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>Обновление заметки</summary>
    [ValidateModel]
    [CheckToken]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateNoteCommand command)
        => Ok(await _mediator.Send(command));
}