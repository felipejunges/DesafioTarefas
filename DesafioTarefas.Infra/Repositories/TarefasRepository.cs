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
            return _db.Tarefas
                .Include(t => t.Historico)
                .FirstOrDefaultAsync(t => t.Id == id);
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

        public async Task AlterarTarefa(Tarefa tarefa)
        {
            await Task.FromResult(_db.Tarefas.Update(tarefa));

            // workaround para incluir os child sem chamar Repository/Context do CommandHandler
            foreach (var historico in tarefa.Historico)
            {
                if (!await _db.Historicos.AnyAsync(h => h.Tarefa.Id == tarefa.Id && h.Id == historico.Id))
                    _db.Historicos.Add(historico);
            }
        }

        public Task ExcluirTarefa(Tarefa tarefa)
        {
            return Task.FromResult(_db.Tarefas.Remove(tarefa));
        }
    }
}
