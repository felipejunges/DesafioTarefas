using DesafioTarefas.Domain.Dtos;
using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> ListarTarefasDoProjeto(Guid projetoId)
        {
            return await _db.Tarefas
                .Include(t => t.Projeto)
                .Where(t => t.Projeto.Id == projetoId)
                .ToListAsync();
        }

        public async Task<RelatorioMediaTarefaConcluidaDto> ObterRelatorioMediaTarefasConcluidas(int quantidadeDias)
        {
            DateOnly dataInicial = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-quantidadeDias));

            var itens = await _db.Tarefas
                .Include(t => t.Projeto)
                .Where(t => 
                    t.DataConclusao != null
                    && t.DataConclusao >= dataInicial
                )
                .GroupBy(t => t.Projeto.UsuarioId)
                .Select(t => new RelatorioMediaTarefaConcluidaItemDto(t.Key, t.Count()))
                .ToListAsync();

            return new RelatorioMediaTarefaConcluidaDto(itens);
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

            foreach (var comentario in tarefa.Comentarios)
            {
                if (!await _db.Comentarios.AnyAsync(h => h.Tarefa.Id == tarefa.Id && h.Id == comentario.Id))
                    _db.Comentarios.Add(comentario);
            }
        }

        public Task ExcluirTarefa(Tarefa tarefa)
        {
            return Task.FromResult(_db.Tarefas.Remove(tarefa));
        }
    }
}
