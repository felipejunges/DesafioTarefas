using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa
{
    public class AlterarTarefaCommandHandler : IRequestHandler<AlterarTarefaCommand, Result>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlterarTarefaCommandHandler(IUserResolverService userResolverService, ITarefaRepository tarefaRepository, IUnitOfWork unitOfWork)
        {
            _userResolverService = userResolverService;
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AlterarTarefaCommand command, CancellationToken cancellationToken)
        {
            var usuarioLogado = await _userResolverService.ObterUsuarioLogado();
            if (usuarioLogado is null)
            {
                return Result.Forbidden();
            }

            var tarefa = await _tarefaRepository.ObterTarefa(command.Id);
            if (tarefa is null)
            {
                return Result.Error("Tarefa inválida");
            }

            tarefa.Atualizar(
                usuarioLogado.Id,
                command.Titulo,
                command.Status,
                command.DataPrazo,
                command.Observacoes);

            await _tarefaRepository.AlterarTarefa(tarefa);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}