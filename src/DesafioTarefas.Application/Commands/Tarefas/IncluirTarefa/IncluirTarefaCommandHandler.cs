using DesafioTarefas.Application.Models.Tarefas;
using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa
{
    public class IncluirTarefaCommandHandler : IRequestHandler<IncluirTarefaCommand, Result<TarefaResponse>>
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncluirTarefaCommandHandler(ITarefaRepository tarefaRepository, IProjetoRepository projetoRepository, IUnitOfWork unitOfWork)
        {
            _tarefaRepository = tarefaRepository;
            _projetoRepository = projetoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TarefaResponse>> Handle(IncluirTarefaCommand command, CancellationToken cancellationToken)
        {
            var projeto = await _projetoRepository.ObterProjeto(command.ProjetoId);
            if (projeto is null)
            {
                return Result<TarefaResponse>.Error("Projeto inválido");
            }

            var tarefa = new Tarefa(
                command.Titulo,
                command.Prioridade,
                command.DataPrazo,
                command.Observacoes);

            var adicionarTarefaResult = projeto.AdicionarTarefa(tarefa);

            if (!adicionarTarefaResult.IsSuccess)
            {
                return Result<TarefaResponse>.Error(adicionarTarefaResult.ErrorMessage ?? string.Empty);
            }

            await _tarefaRepository.IncluirTarefa(tarefa);

            await _unitOfWork.SaveChangesAsync();

            return Result<TarefaResponse>.Success(TarefaResponseMapper.Map(tarefa));
        }
    }
}
