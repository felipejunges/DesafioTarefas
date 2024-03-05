namespace DesafioTarefas.Domain.Entities
{
    public class Historico
    {
        public Guid Id { get; private set; }

        public Tarefa Tarefa { get; private set; } = null!;
    }
}
