using DesafioTarefas.Application.Models.Tarefas;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.ListarTarefas
{
    public record ListarTarefasQuery(Guid ProjetoId) : IRequest<IEnumerable<TarefaResponse>>;
}
