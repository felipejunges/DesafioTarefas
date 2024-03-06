using DesafioTarefas.Application.Models.Projetos;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.ListarProjetos
{
    public class ListarProjetosQueryHandler : IRequestHandler<ListarProjetosQuery, IEnumerable<ProjetoResponse>>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IProjetoRepository _projetoRepository;

        public ListarProjetosQueryHandler(IUserResolverService userResolverService, IProjetoRepository projetoRepository)
        {
            _userResolverService = userResolverService;
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<ProjetoResponse>> Handle(ListarProjetosQuery query, CancellationToken cancellationToken)
        {
            var usuarioLogado = await _userResolverService.ObterUsuarioLogado();
            if (usuarioLogado is null)
            {
                return Enumerable.Empty<ProjetoResponse>();
            }

            var projetos = await _projetoRepository.ListarProjetos(usuarioLogado.Id);

            return ProjetoResponseMapper.Map(projetos);
        }
    }
}
