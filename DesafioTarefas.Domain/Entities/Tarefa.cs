using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; private set; }

        public Prioridade Prioridade { get; private set; }

        public Status Status { get; private set; }

        public Projeto Projeto { get; private set; } = null!;

        public bool Concluida => Status == Status.Concluida;

        public IEnumerable<Historico> Historico { get; private set; } = null!;

        public IEnumerable<Comentario> Comentario { get; private set; } = null!;

        private Tarefa()
        {
        }

        public Tarefa(Guid id, Prioridade prioridade)
            : this(
                id,
                prioridade,
                Status.Pendente)
        {
        }

        public Tarefa(Guid id, Prioridade prioridade, Status status)
        {
            Id = id;
            Prioridade = prioridade;
            Status = status;
        }
    }
}
