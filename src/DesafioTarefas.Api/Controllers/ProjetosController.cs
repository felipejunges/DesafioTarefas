using DesafioTarefas.Application.Commands.Projetos.ExcluirProjeto;
using DesafioTarefas.Application.Commands.Projetos.IncluirProjeto;
using DesafioTarefas.Application.Commands.Projetos.ListarProjetos;
using DesafioTarefas.Application.Models.Projetos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarefas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjetosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjetoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjetoResponse>>> ListarProjetos()
        {
            var result = await _mediator.Send(new ListarProjetosQuery());

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjetoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjetoResponse>> IncluirProjeto([FromBody] IncluirProjetoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Created("[controller]/{id}", result.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirProjeto([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new ExcluirProjetoCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
