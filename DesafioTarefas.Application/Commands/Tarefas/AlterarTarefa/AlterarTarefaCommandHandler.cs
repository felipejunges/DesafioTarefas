using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa
{
    public class AlterarTarefaCommandHandler : IRequestHandler<AlterarTarefaCommand, Result>
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlterarTarefaCommandHandler(ITarefaRepository tarefaRepository, IUnitOfWork unitOfWork)
        {
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AlterarTarefaCommand command, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.ObterTarefa(command.Id);
            if (tarefa is null)
            {
                return Result.Error("Tarefa inválida");
            }

            tarefa.Atualizar(
                command.Titulo,
                command.Status,
                command.Observacoes);

            await _tarefaRepository.AlterarTarefa(tarefa);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
