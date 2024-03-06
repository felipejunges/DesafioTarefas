using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Tests.Domain.Entities
{
    public class ProjetoTests
    {
        [Fact]
        public void DeveSerPossivelAdicionarTarefaEmProjetoDisponivel()
        {
            var tarefas = Enumerable.Repeat(new Tarefa(Guid.NewGuid(), "Tarefa X", Prioridade.Media, Status.EmAndamento, string.Empty), Projeto.LIMITE_TAREFAS - 1).ToList();
            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.AdicionarTarefa(new Tarefa(Guid.NewGuid(), "Tarefa X", Prioridade.Media, Status.Pendente, string.Empty));

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelAdicionarTarefaEmProjetoLotado()
        {
            var tarefas = Enumerable.Repeat(new Tarefa(Guid.NewGuid(), "Tarefa X", Prioridade.Media, Status.EmAndamento, string.Empty), Projeto.LIMITE_TAREFAS).ToList();
            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.AdicionarTarefa(new Tarefa(Guid.NewGuid(), "Tarefa X", Prioridade.Media, Status.Pendente, string.Empty));

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeSemTarefas()
        {
            var tarefas = new List<Tarefa>();
            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.ValidarPodeSerRemovido();

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeSemTarefasPendentes()
        {
            var tarefas = new List<Tarefa>()
            {
                new Tarefa(Guid.NewGuid(), "Tarefa 1", Prioridade.Media, Status.Concluida, string.Empty),
                new Tarefa(Guid.NewGuid(), "Tarefa 2", Prioridade.Media, Status.Concluida, string.Empty)
            };

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.ValidarPodeSerRemovido();

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeComTarefasPendentes()
        {
            var tarefas = new List<Tarefa>()
            {
                new Tarefa(Guid.NewGuid(), "Tarefa 1", Prioridade.Media, Status.Concluida, string.Empty),
                new Tarefa(Guid.NewGuid(), "Tarefa 2", Prioridade.Media, Status.Pendente, string.Empty)
            };

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.ValidarPodeSerRemovido();

            Assert.False(result.IsSuccess);
        }
    }
}
