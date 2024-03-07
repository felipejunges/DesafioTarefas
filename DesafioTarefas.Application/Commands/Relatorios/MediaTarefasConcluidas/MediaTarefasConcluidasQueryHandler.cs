using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Dtos;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using MediatR;

namespace DesafioTarefas.Application.Commands.Relatorios.MediaTarefasConcluidas
{
    public class MediaTarefasConcluidasQueryHandler : IRequestHandler<MediaTarefasConcluidasQuery, Result<RelatorioMediaTarefaConcluidaDto>>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly ITarefaRepository _tarefaRepository;

        public MediaTarefasConcluidasQueryHandler(IUserResolverService userResolverService, ITarefaRepository tarefaRepository)
        {
            _userResolverService = userResolverService;
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result<RelatorioMediaTarefaConcluidaDto>> Handle(MediaTarefasConcluidasQuery query, CancellationToken cancellationToken)
        {
            var usuarioLogado = await _userResolverService.ObterUsuarioLogado();
            if (usuarioLogado is null || !usuarioLogado.Admin)
            {
                return Result<RelatorioMediaTarefaConcluidaDto>.Forbidden();
            }

            var relatorio = await _tarefaRepository.ObterRelatorioMediaTarefasConcluidas(query.QuantidadeDias);

            return Result<RelatorioMediaTarefaConcluidaDto>.Success(relatorio);
        }
    }
}
