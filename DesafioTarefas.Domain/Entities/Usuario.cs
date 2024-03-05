namespace DesafioTarefas.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }

        public IEnumerable<Projeto> Projetos { get; private set; } = null!;
    }
}
