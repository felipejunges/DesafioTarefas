namespace DesafioTarefas.Domain.Entities
{
    public class Comentario
    {
        public Guid Id { get; private set; }

        public Guid UsuarioId { get; private set; }

        public DateTime DataHora { get; private set; }

        public string Texto { get; private set; }

        public Tarefa Tarefa { get; private set; } = null!;

        private Comentario()
        {
            Texto = string.Empty;
        }

        public Comentario(Guid usuarioId, string texto, Tarefa tarefa)
            : this(
                Guid.NewGuid(),
                usuarioId,
                DateTime.Now,
                texto,
                tarefa)
        {
        }

        public Comentario(Guid id, Guid usuarioId, DateTime dataHora, string texto, Tarefa tarefa)
        {
            Id = id;
            UsuarioId = usuarioId;
            DataHora = dataHora;
            Texto = texto;
            Tarefa = tarefa;
        }
    }
}
