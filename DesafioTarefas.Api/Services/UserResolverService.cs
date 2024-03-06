using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.ValueObject;

namespace DesafioTarefas.Api.Services
{
    public class UserResolverService : IUserResolverService
    {
        public async Task<Usuario?> ObterUsuarioLogado()
        {
            return await Task.FromResult(
                new Usuario(
                    new Guid("ed612925-7217-416a-b5a3-999d4b6b2fac"),
                    "Felipe J."));
        }
    }
}
