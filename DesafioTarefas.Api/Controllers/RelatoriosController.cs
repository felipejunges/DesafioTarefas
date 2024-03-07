using DesafioTarefas.Application.Commands.Relatorios.MediaTarefasConcluidas;
using DesafioTarefas.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarefas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/[controller]/MediaTarefas")]
        [ProducesResponseType(typeof(RelatorioMediaTarefaConcluidaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RelatorioMediaTarefaConcluidaDto>> ObterRelatorioMediaTarefas([FromQuery] MediaTarefasConcluidasQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result.Value);
        }
    }
}
