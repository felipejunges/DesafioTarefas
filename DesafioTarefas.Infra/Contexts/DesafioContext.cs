using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Infra.Contexts.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarefas.Infra.Contexts
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options) { }

        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<Historico> Historicos => Set<Historico>();
        public DbSet<Projeto> Projetos => Set<Projeto>();
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComentarioMap());
            modelBuilder.ApplyConfiguration(new HistoricoMap());
            modelBuilder.ApplyConfiguration(new ProjetoMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
