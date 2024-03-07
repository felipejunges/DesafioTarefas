using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirComentario
{
    public class IncluirComentarioCommandHandler : IRequestHandler<IncluirComentarioCommand, Result>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncluirComentarioCommandHandler(IUserResolverService userResolverService, ITarefaRepository tarefaRepository, IUnitOfWork unitOfWork)
        {
            _userResolverService = userResolverService;
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(IncluirComentarioCommand command, CancellationToken cancellationToken)
        {
            var usuarioLogado = await _userResolverService.ObterUsuarioLogado();
            if (usuarioLogado is null)
            {
                return Result.Forbidden();
            }

            var tarefa = await _tarefaRepository.ObterTarefa(command.TarefaId);
            if (tarefa is null)
            {
                return Result.Error("Tarefa inválida");
            }

            tarefa.AdicionarComentario(usuarioLogado.Id, command.Texto);

            await _tarefaRepository.AlterarTarefa(tarefa);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
