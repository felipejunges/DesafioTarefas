using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; private set; }

        public string Titulo { get; private set; }

        public Prioridade Prioridade { get; private set; }

        public Status Status { get; private set; }

        public string? Observacoes { get; private set; }

        public Projeto Projeto { get; private set; } = null!;

        public bool Concluida => Status == Status.Concluida;

        public IEnumerable<Historico> Historico { get; private set; } = null!;

        public IEnumerable<Comentario> Comentario { get; private set; } = null!;

        private Tarefa()
        {
            Titulo = string.Empty;
        }

        public Tarefa(string titulo, Prioridade prioridade, string? observacoes)
            : this(
                Guid.NewGuid(),
                titulo,
                prioridade,
                Status.Pendente,
                observacoes)
        {
        }

        public Tarefa(Guid id, string titulo, Prioridade prioridade, Status status, string? observacoes)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            Status = status;
            Observacoes = observacoes;
        }

        public void VincularProjeto(Projeto projeto)
        {
            Projeto = projeto;
        }

        public void Atualizar(string titulo, Status status, string? observacoes)
        {
            Titulo = titulo;
            Status = status;
            Observacoes = observacoes;
        }
    }
}
