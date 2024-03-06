using DesafioTarefas.Domain.ValueObject;

namespace DesafioTarefas.Domain.Services
{
    public interface IUserResolverService
    {
        Task<Usuario?> ObterUsuarioLogado();
    }
}
