using DesafioTarefas.Domain.Entities;

namespace DesafioTarefas.Domain.Repositories
{
    public interface IProjetoRepository
    {
        Task<Projeto?> ObterProjeto(Guid id);
        Task<IEnumerable<Projeto>> ListarProjetos(Guid usuarioId);
        Task IncluirProjeto(Projeto projeto);
        Task ExcluirProjeto(Projeto projeto);
    }
}
