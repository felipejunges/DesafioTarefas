using DesafioTarefas.Application.Commands.Projetos.ExcluirProjeto;
using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.UnitsOfWork;
using Moq;

namespace DesafioTarefas.Tests.Application.Commands.Projetos.ExcluirProjeto
{
    public class ExcluirProjetoCommandHandlerTests
    {
        private readonly Mock<IProjetoRepository> _projetoRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public ExcluirProjetoCommandHandlerTests()
        {
            _projetoRepository = new Mock<IProjetoRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task NaoDeveSerPossivelExcluirProjetoSeNaoEncontrado()
        {
            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync((Projeto?)null);

            var command = new ExcluirProjetoCommand(Guid.NewGuid());

            var handler = new ExcluirProjetoCommandHandler(_projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            _projetoRepository.Verify(v => v.ExcluirProjeto(It.IsAny<Projeto>()), Times.Never);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task NaoDeveSerPossivelExcluirProjetoComTarefaPendente()
        {
            var tarefas = new List<Tarefa>()
            {
                Tarefa.CriarTarefaParaTeste(Status.Concluida),
                Tarefa.CriarTarefaParaTeste(Status.Pendente)
            };

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync(projeto);

            var command = new ExcluirProjetoCommand(projeto.Id);

            var handler = new ExcluirProjetoCommandHandler(_projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            _projetoRepository.Verify(v => v.ExcluirProjeto(It.IsAny<Projeto>()), Times.Never);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeveSerPossivelExcluirProjetoSemTarefaPendente()
        {
            var tarefas = new List<Tarefa>()
            {
                Tarefa.CriarTarefaParaTeste(Status.Concluida),
                Tarefa.CriarTarefaParaTeste(Status.Concluida)
            };

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            _projetoRepository.Setup(s => s.ObterProjeto(It.IsAny<Guid>())).ReturnsAsync(projeto);

            var command = new ExcluirProjetoCommand(projeto.Id);

            var handler = new ExcluirProjetoCommandHandler(_projetoRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            _projetoRepository.Verify(v => v.ExcluirProjeto(It.IsAny<Projeto>()), Times.Once);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
        }
    }
}