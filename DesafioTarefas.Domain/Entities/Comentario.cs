namespace DesafioTarefas.Domain.Entities
{
    public class Comentario
    {
        public Guid Id { get; private set; }

        public Tarefa Tarefa { get; private set; } = null!;
    }
}
