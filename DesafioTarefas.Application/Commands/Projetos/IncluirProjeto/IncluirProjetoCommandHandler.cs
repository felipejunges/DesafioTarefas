using DesafioTarefas.Application.Models.Projetos;
using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.IncluirProjeto
{
    public class IncluirProjetoCommandHandler : IRequestHandler<IncluirProjetoCommand, Result<ProjetoResponse>>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncluirProjetoCommandHandler(IUserResolverService userResolverService, IProjetoRepository projetoRepository, IUnitOfWork unitOfWork)
        {
            _userResolverService = userResolverService;
            _projetoRepository = projetoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProjetoResponse>> Handle(IncluirProjetoCommand command, CancellationToken cancellationToken)
        {
            var usuarioLogado = await _userResolverService.ObterUsuarioLogado();
            if (usuarioLogado is null)
            {
                return Result<ProjetoResponse>.Forbidden();
            }

            var projeto = new Projeto(usuarioLogado.Id);

            await _projetoRepository.IncluirProjeto(projeto);

            await _unitOfWork.SaveChangesAsync();

            return Result<ProjetoResponse>.Success(ProjetoResponseMapper.Map(projeto));
        }
    }
}
