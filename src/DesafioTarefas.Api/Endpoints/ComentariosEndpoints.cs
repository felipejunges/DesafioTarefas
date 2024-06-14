using DesafioTarefas.Application.Commands.Tarefas.IncluirComentario;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DesafioTarefas.Api.Endpoints
{
    public static class ComentariosEndpoints
    {
        private static readonly string GROUP_ENDPOINT = "/api/Projetos/{projetoId}/Tarefas/{tarefaId}/Comentarios";

        public static void MapComentariosEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup(GROUP_ENDPOINT)
                .WithOpenApi()
                .WithTags("Comentarios");

            endpoints.MapPost("/", IncluirComentario);
        }

        private static async Task<Results<Created, BadRequest<string>, ValidationProblem>> IncluirComentario(
            Guid projetoId,
            Guid tarefaId,
            IncluirComentarioCommand command,
            IValidator<IncluirComentarioCommand> validator,
            IMediator mediator)
        {
            var validation = validator.Validate(command);
            if (!validation.IsValid)
            {
                return TypedResults.ValidationProblem(validation.ToDictionary());
            }

            var result = await mediator.Send(command.Agregar(projetoId, tarefaId));

            return result.IsSuccess
                ? TypedResults.Created()
                : TypedResults.BadRequest(result.ErrorMessage);
        }
    }
}