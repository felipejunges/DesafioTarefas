using DesafioTarefas.Application.Commands.Projetos.ListarProjetos;
using DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa;
using DesafioTarefas.Application.Commands.Tarefas.ExcluirTarefa;
using DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa;
using DesafioTarefas.Application.Commands.Tarefas.ListarTarefas;
using DesafioTarefas.Application.Models.Projetos;
using DesafioTarefas.Application.Models.Tarefas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarefas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TarefasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/Projetos/{projetoId}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<ProjetoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjetoResponse>>> ListarTarefas([FromRoute] Guid projetoId)
        {
            var result = await _mediator.Send(new ListarTarefasQuery(projetoId));

            return Ok(result);
        }

        [HttpPost("/api/Projetos/{projetoId}/[controller]")]
        [ProducesResponseType(typeof(TarefaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TarefaResponse>> IncluirTarefa([FromRoute] Guid projetoId, [FromBody] IncluirTarefaCommand command)
        {
            var result = await _mediator.Send(command.Agregar(projetoId));

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Created("[controller]/{id}", result.Value);
        }

        [HttpPut("/api/Projetos/{projetoId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AlterarTarefa([FromRoute] Guid projetoId, [FromRoute] Guid id, [FromBody] AlterarTarefaCommand command)
        {
            var result = await _mediator.Send(command.Agregar(projetoId, id));

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("/api/Projetos/{projetoId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirTarefa([FromRoute] Guid projetoId, [FromRoute] Guid id, [FromBody] ExcluirTarefaCommand command)
        {
            var result = await _mediator.Send(command.Agregar(projetoId, id));

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
