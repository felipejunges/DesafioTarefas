using DesafioTarefas.Domain;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirComentario
{
    public class IncluirComentarioCommand : IRequest<Result>
    {
        public Guid TarefaId { get; private set; }

        public Guid ProjetoId { get; private set; }

        public string Texto { get; init; } = string.Empty;

        public IncluirComentarioCommand Agregar(Guid projetoId, Guid tarefaId)
        {
            TarefaId = tarefaId;
            ProjetoId = projetoId;

            return this;
        }
    }
}
