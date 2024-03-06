using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Tests.Domain.Entities
{
    public class ProjetoTests
    {
        [Fact]
        public void DeveSerPossivelAdicionarTarefaEmProjetoDisponivel()
        {
            var tarefas = Enumerable.Repeat(Tarefa.CriarTarefaParaTeste(Status.EmAndamento), Projeto.LIMITE_TAREFAS - 1).ToList();
            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.AdicionarTarefa(Tarefa.CriarTarefaParaTeste(Status.Pendente));

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelAdicionarTarefaEmProjetoLotado()
        {
            var tarefas = Enumerable.Repeat(
                    Tarefa.CriarTarefaParaTeste(Status.EmAndamento),
                    Projeto.LIMITE_TAREFAS)
                .ToList();
            
            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.AdicionarTarefa(Tarefa.CriarTarefaParaTeste(Status.Pendente));

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
                Tarefa.CriarTarefaParaTeste(Status.Concluida),
                Tarefa.CriarTarefaParaTeste(Status.Concluida)
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
                Tarefa.CriarTarefaParaTeste(Status.Concluida),
                Tarefa.CriarTarefaParaTeste(Status.Pendente)
            };

            var projeto = new Projeto(Guid.NewGuid(), DateTime.Now, tarefas, Guid.NewGuid());

            var result = projeto.ValidarPodeSerRemovido();

            Assert.False(result.IsSuccess);
        }
    }
}
