using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Enums;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa
{
    public class AlterarTarefaCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Guid ProjetoId { get;private set; }
        public string Titulo { get; init; } = string.Empty;
        public Status Status { get; init; }
        public string? Observacoes { get; init; }

        public AlterarTarefaCommand Agregar(Guid projetoId, Guid id)
        {
            Id = id;
            ProjetoId = projetoId;

            return this;
        }
    }
}
