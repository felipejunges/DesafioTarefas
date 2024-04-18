using DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa;
using DesafioTarefas.Application.Commands.Tarefas.ExcluirTarefa;
using DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa;
using DesafioTarefas.Application.Commands.Tarefas.ListarTarefas;
using DesafioTarefas.Application.Models.Tarefas;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DesafioTarefas.Api.Endpoints
{
    public static class TarefasEndpoints
    {
        private static readonly string GROUP_ENDPOINT = "/api/Projetos/{projetoId}/Tarefas";

        public static void MapTarefasEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup(GROUP_ENDPOINT)
                .WithOpenApi()
                .WithTags("Tarefas");

            endpoints.MapGet("/", ListarTarefas);
            endpoints.MapPost("/", IncluirTarefa);
            endpoints.MapPut("/{id}", AlterarTarefa);
            endpoints.MapDelete("/{id}", ExcluirTarefa);
        }

        private static async Task<IEnumerable<TarefaResponse>> ListarTarefas(Guid projetoId, IMediator mediator)
        {
            return await mediator.Send(new ListarTarefasQuery(projetoId));
        }

        private static async Task<Results<Created<TarefaResponse>, BadRequest<string>>> IncluirTarefa(Guid projetoId, IncluirTarefaCommand command, IMediator mediator)
        {
            var result = await mediator.Send(command.Agregar(projetoId));

            return result.IsSuccess
                ? TypedResults.Created($"{GROUP_ENDPOINT}/{result.Value!.Id}", result.Value)
                : TypedResults.BadRequest(result.ErrorMessage);

        }

        private static async Task<Results<Ok, BadRequest<string>>> AlterarTarefa(Guid projetoId, Guid id, AlterarTarefaCommand command, IMediator mediator)
        {
            var result = await mediator.Send(command.Agregar(projetoId, id));

            return result.IsSuccess
                ? TypedResults.Ok()
                : TypedResults.BadRequest(result.ErrorMessage);

        }

        private static async Task<Results<NoContent, BadRequest<string>>> ExcluirTarefa(Guid projetoId, Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new ExcluirTarefaCommand(projetoId, id));

            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result.ErrorMessage);
        }

        //[HttpPost("/api/Projetos/{projetoId}/[controller]/{id}/Comentario")]
        //[ProducesResponseType(typeof(TarefaResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<TarefaResponse>> IncluirComentario([FromRoute] Guid projetoId, Guid id, [FromBody] IncluirComentarioCommand command)
        //{
        //    var result = await _mediator.Send(command.Agregar(projetoId, id));

        //    if (!result.IsSuccess)
        //        return BadRequest(result.ErrorMessage);

        //    return Ok();
        //}
    }
}