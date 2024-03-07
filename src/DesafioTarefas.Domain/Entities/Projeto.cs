namespace DesafioTarefas.Domain.Entities
{
    public class Projeto
    {
        public const int LIMITE_TAREFAS = 20;

        public Guid Id { get; private set; }

        public DateTime DataHoraCadastro { get; private set; }

        public ICollection<Tarefa> Tarefas { get; private set; } = null!;

        public Guid UsuarioId { get; private set; }

        private Projeto()
        {
        }

        public Projeto(Guid usuarioId)
            : this(
                Guid.NewGuid(),
                DateTime.Now,
                new List<Tarefa>(),
                usuarioId)
        {
        }

        public Projeto(Guid id, DateTime dataHoraCadastro, ICollection<Tarefa> tarefas, Guid usuarioId)
        {
            Id = id;
            DataHoraCadastro = dataHoraCadastro;
            Tarefas = tarefas;
            UsuarioId = usuarioId;
        }

        public Result AdicionarTarefa(Tarefa tarefa)
        {
            if (Tarefas.Count >= LIMITE_TAREFAS)
            {
                return Result.Error($"O limite de tarefas do projeto ({LIMITE_TAREFAS}) foi atingido.");
            }

            Tarefas.Add(tarefa);

            return Result.Success();
        }

        public Result ValidarPodeSerRemovido()
        {
            if (Tarefas.Any(t => !t.Concluida))
            {
                return Result.Error("O projeto não pode ser removido pois possui tarefas pendentes associadas. Conclua ou remova as tarefas pendentes.");
            }

            return Result.Success();
        }
    }
}
