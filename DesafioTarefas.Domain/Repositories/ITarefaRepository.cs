using DesafioTarefas.Domain.Dtos;
using DesafioTarefas.Domain.Entities;

namespace DesafioTarefas.Domain.Repositories
{
    public interface ITarefaRepository
    {
        Task<Tarefa?> ObterTarefa(Guid id);
        Task<IEnumerable<Tarefa>> ListarTarefasDoProjeto(Guid projetoId);
        Task IncluirTarefa(Tarefa tarefa);
        Task AlterarTarefa(Tarefa tarefa);
        Task ExcluirTarefa(Tarefa tarefa);
        Task<RelatorioMediaTarefaConcluidaDto> ObterRelatorioMediaTarefasConcluidas(int quantidadeDias);
    }
}
