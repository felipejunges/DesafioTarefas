using DesafioTarefas.Application.Commands.Projetos.ExcluirProjeto;
using DesafioTarefas.Application.Commands.Projetos.IncluirProjeto;
using DesafioTarefas.Application.Commands.Projetos.ListarProjetos;
using DesafioTarefas.Application.Models.Projetos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DesafioTarefas.Api.Endpoints
{
    public static class ProjetosEndpoints
    {
        private static readonly string GROUP_ENDPOINT = "/api/Projetos";

        public static void MapProjetosEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup(GROUP_ENDPOINT)
                .WithOpenApi()
                .WithTags("Projetos");

            endpoints.MapGet("/", ListarProjetos);
            endpoints.MapPost("/", IncluirProjeto);
            endpoints.MapDelete("/{id}", ExcluirProjeto);
        }

        private static async Task<IEnumerable<ProjetoResponse>> ListarProjetos(IMediator mediator)
        {
            return await mediator.Send(new ListarProjetosQuery());
        }

        private static async Task<Results<Created<ProjetoResponse>, BadRequest<string>>> IncluirProjeto(IncluirProjetoCommand command, IMediator mediator)
        {
            var result = await mediator.Send(command);

            return result.IsSuccess
                ? TypedResults.Created($"{GROUP_ENDPOINT}/{result.Value!.Id}", result.Value)
                : TypedResults.BadRequest(result.ErrorMessage);
        }

        private static async Task<Results<NoContent, BadRequest<string>>> ExcluirProjeto(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new ExcluirProjetoCommand(id));

            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result.ErrorMessage);
        }
    }
}