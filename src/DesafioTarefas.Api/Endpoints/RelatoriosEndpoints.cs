using DesafioTarefas.Application.Commands.Relatorios.MediaTarefasConcluidas;
using DesafioTarefas.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DesafioTarefas.Api.Endpoints
{
    public static class RelatoriosEndpoints
    {
        private static readonly string GROUP_ENDPOINT = "/api/Relatorios";

        public static void MapRelatoriosEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup(GROUP_ENDPOINT)
                .WithOpenApi()
                .WithTags("Relatorios");

            endpoints.MapGet("/MediaTarefas", ObterRelatorioMediaTarefas);
        }

        private static async Task<Results<Ok<RelatorioMediaTarefaConcluidaDto>, ForbidHttpResult>> ObterRelatorioMediaTarefas([AsParameters] MediaTarefasConcluidasQuery query, IMediator mediator)
        {
            var result = await mediator.Send(query);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.Forbid();
        }
    }
}