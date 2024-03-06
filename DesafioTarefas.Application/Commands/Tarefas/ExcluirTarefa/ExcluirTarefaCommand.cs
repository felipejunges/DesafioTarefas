using DesafioTarefas.Domain;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.ExcluirTarefa
{
    public class ExcluirTarefaCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Guid ProjetoId { get; private set; }

        public ExcluirTarefaCommand Agregar(Guid projetoId, Guid id)
        {
            Id = id;
            ProjetoId = projetoId;

            return this;
        }
    }
}
