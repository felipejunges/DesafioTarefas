namespace DesafioTarefas.Domain.Entities
{
    public class Historico
    {
        public Guid Id { get; private set; }

        public Guid UsuarioId { get; private set; }

        public DateTime DataHora { get; private set; }

        public string Descricao { get; private set; }

        public Tarefa Tarefa { get; private set; } = null!;

        private Historico()
        {
            Descricao = string.Empty;
        }

        public Historico(Guid usuarioId, string descricao, Tarefa tarefa)
            : this(
                Guid.NewGuid(),
                usuarioId,
                DateTime.Now,
                descricao,
                tarefa)
        {
        }

        public Historico(Guid id, Guid usuarioId, DateTime dataHora, string descricao, Tarefa tarefa)
        {
            Id = id;
            UsuarioId = usuarioId;
            DataHora = dataHora;
            Descricao = descricao;
            Tarefa = tarefa;
        }
    }
}
