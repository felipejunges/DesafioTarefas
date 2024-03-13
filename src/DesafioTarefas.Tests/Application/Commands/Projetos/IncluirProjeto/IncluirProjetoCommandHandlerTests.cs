using DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa;
using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using Moq;

namespace DesafioTarefas.Tests.Application.Commands.Projetos.IncluirProjeto
{
    public class IncluirProjetoCommandHandlerTests
    {
        private readonly Mock<IProjetoRepository> _projetoRepository;
        private readonly Mock<ITarefaRepository> _tarefaRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public IncluirProjetoCommandHandlerTests(Mock<IProjetoRepository> projetoRepository, Mock<ITarefaRepository> tarefaRepository, Mock<IUnitOfWork> unitOfWork)
        {
            _projetoRepository = projetoRepository;
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CasoProjetoInexistenteCommandDeveFalhar()
        {
            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync((Projeto?)null);

            var command = new IncluirTarefaCommand()
            {
                Prioridade = Prioridade.Baixa,
                DataPrazo = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1))
            };

            var handler = new IncluirTarefaCommandHandler(_tarefaRepository.Object, _projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.Equal("Projeto inválido", result.ErrorMessage);
            _tarefaRepository.Verify(v => v.IncluirTarefa(It.IsAny<Tarefa>()), Times.Never);
        }

        public async Task CasoProjetoTenha20TarefasCommandDeveFalhar()
        {
            var tarefas = Enumerable.Repeat(
                    Tarefa.CriarTarefaParaTeste(Status.EmAndamento),
                    Projeto.LIMITE_TAREFAS)
                .ToList();

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync(projeto);

            var command = new IncluirTarefaCommand()
            {
                Prioridade = Prioridade.Baixa,
                DataPrazo = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1))
            };

            var handler = new IncluirTarefaCommandHandler(_tarefaRepository.Object, _projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.NotEqual("Projeto inválido", result.ErrorMessage);
            _tarefaRepository.Verify(v => v.IncluirTarefa(It.IsAny<Tarefa>()), Times.Never);
        }

        public async Task CasoProjetoTenha19TarefasCommandDeveSerBemSucedido()
        {
            var tarefas = Enumerable.Repeat(
                    Tarefa.CriarTarefaParaTeste(Status.EmAndamento),
                    Projeto.LIMITE_TAREFAS - 1)
                .ToList();

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync(projeto);

            var command = new IncluirTarefaCommand()
            {
                Prioridade = Prioridade.Baixa,
                DataPrazo = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1))
            };

            var handler = new IncluirTarefaCommandHandler(_tarefaRepository.Object, _projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            _tarefaRepository.Verify(v => v.IncluirTarefa(It.IsAny<Tarefa>()), Times.Once);
        }
    }
}
