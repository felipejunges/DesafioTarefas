using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.ExcluirProjeto
{
    public class ExcluirProjetoCommandHandler : IRequestHandler<ExcluirProjetoCommand, Result>
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirProjetoCommandHandler(IProjetoRepository projetoRepository, IUnitOfWork unitOfWork)
        {
            _projetoRepository = projetoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ExcluirProjetoCommand command, CancellationToken cancellationToken)
        {
            var projeto = await _projetoRepository.ObterProjeto(command.Id);
            if (projeto is null)
            {
                return Result.Error("Projeto inválido");
            }

            var resultValidacao = projeto.ValidarPodeSerRemovido();
            if (!resultValidacao.IsSuccess)
            {
                return resultValidacao;
            }

            await _projetoRepository.ExcluirProjeto(projeto);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
