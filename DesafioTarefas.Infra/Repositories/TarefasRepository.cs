using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarefas.Infra.Repositories
{
    public class TarefasRepository : ITarefaRepository
    {
        private readonly DesafioContext _db;

        public TarefasRepository(DesafioContext db)
        {
            _db = db;
        }

        public Task<Tarefa?> ObterTarefa(Guid id)
        {
            return _db.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> ListarTarefasDoProjeto(Guid projetoId)
        {
            return await _db.Tarefas
                .Include(t => t.Projeto)
                .Where(t => t.Projeto.Id == projetoId)
                .ToListAsync();
        }

        public Task IncluirTarefa(Tarefa tarefa)
        {
            return _db.Tarefas.AddAsync(tarefa).AsTask();
        }

        public Task AlterarTarefa(Tarefa tarefa)
        {
            return Task.FromResult(_db.Tarefas.Update(tarefa));
        }

        public Task ExcluirTarefa(Tarefa tarefa)
        {
            return Task.FromResult(_db.Tarefas.Remove(tarefa));
        }
    }
}
