using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarefas.Infra.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly DesafioContext _db;

        public ProjetoRepository(DesafioContext db)
        {
            _db = db;
        }

        public Task<Projeto?> ObterProjeto(Guid id)
        {
            return _db.Projetos
                .Include(p => p.Tarefas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Projeto>> ListarProjetos(Guid usuarioId)
        {
            return await _db.Projetos
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task IncluirProjeto(Projeto projeto)
        {
            await _db.Projetos.AddAsync(projeto);
        }

        public Task ExcluirProjeto(Projeto projeto)
        {
            return Task.FromResult(_db.Projetos.Remove(projeto));
        }
    }
}
