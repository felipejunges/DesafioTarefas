namespace DesafioTarefas.Domain.Entities
{
    public class Historico
    {
        public Guid Id { get; private set; }

        public Guid UsuarioId { get; private set; }

        public DateTime DataHora { get; private set; }

        public string Campo { get; private set; }

        public string? ValorOriginal { get; private set; }

        public string? NovoValor { get; private set; }

        public Tarefa Tarefa { get; private set; } = null!;

        private Historico()
        {
            Campo = string.Empty;
        }

        public Historico(Guid usuarioId, string campo, string? valorOriginal, string? novoValor, Tarefa tarefa)
            : this(
                Guid.NewGuid(),
                usuarioId,
                DateTime.Now,
                campo,
                valorOriginal,
                novoValor,
                tarefa)
        {
        }

        public Historico(Guid id, Guid usuarioId, DateTime dataHora, string campo, string? valorOriginal, string? novoValor, Tarefa tarefa)
        {
            Id = id;
            UsuarioId = usuarioId;
            DataHora = dataHora;
            Campo = campo;
            ValorOriginal = valorOriginal;
            NovoValor = novoValor;
            Tarefa = tarefa;
        }
    }
}
