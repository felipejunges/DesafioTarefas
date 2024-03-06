using DesafioTarefas.Application.Models.Tarefas;
using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Enums;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa
{
    public class IncluirTarefaCommand : IRequest<Result<TarefaResponse>>
    {
        public Guid ProjetoId { get; private set; }
        public string Titulo { get; init; } = string.Empty;
        public Prioridade Prioridade { get; init; }
        public string? Observacoes { get; init; }

        public IncluirTarefaCommand Agregar(Guid projetoId)
        {
            ProjetoId = projetoId;

            return this;
        }
    }
}
