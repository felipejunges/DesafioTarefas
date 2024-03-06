using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.ExcluirTarefa
{
    public class ExcluirTarefaCommandHandler : IRequestHandler<ExcluirTarefaCommand, Result>
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirTarefaCommandHandler(ITarefaRepository tarefaRepository, IUnitOfWork unitOfWork)
        {
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ExcluirTarefaCommand command, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.ObterTarefa(command.Id);
            if (tarefa is null)
            {
                return Result.Error("Tarefa inválida");
            }
            
            await _tarefaRepository.ExcluirTarefa(tarefa);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
