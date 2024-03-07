using DesafioTarefas.Domain;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.ExcluirProjeto
{
    public record ExcluirProjetoCommand(Guid Id) : IRequest<Result>;
}
