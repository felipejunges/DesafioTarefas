using DesafioTarefas.Domain.Enums;
using System.ComponentModel;

namespace DesafioTarefas.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; private set; }

        public string Titulo { get; private set; }

        public Prioridade Prioridade { get; private set; }

        public Status Status { get; private set; }

        public DateTime DataPrazo { get; private set; }

        public string? Observacoes { get; private set; }

        public Projeto Projeto { get; private set; } = null!;

        public bool Concluida => Status == Status.Concluida;

        public ICollection<Historico> Historico { get; private set; }

        public IEnumerable<Comentario> Comentario { get; private set; } = null!;

        private Tarefa()
        {
            Titulo = string.Empty;
            Historico = new HashSet<Historico>();
        }

        public Tarefa(string titulo, Prioridade prioridade, DateTime dataPrazo, string? observacoes)
            : this(
                Guid.NewGuid(),
                titulo,
                prioridade,
                Status.Pendente,
                dataPrazo,
                observacoes)
        {
        }

        public Tarefa(Guid id, string titulo, Prioridade prioridade, Status status, DateTime dataPrazo, string? observacoes)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            Status = status;
            DataPrazo = dataPrazo;
            Observacoes = observacoes;
            
            Historico = new HashSet<Historico>();
        }

        public static Tarefa CriarTarefaParaTeste(Status status = Status.Pendente) =>
            new Tarefa(
                Guid.NewGuid(),
                "Tarefa X",
                Prioridade.Media,
                status,
                DateTime.Now.Date.AddDays(1),
                string.Empty);

        public void VincularProjeto(Projeto projeto)
        {
            Projeto = projeto;
        }

        public void Atualizar(Guid usuarioId, string titulo, Status status, DateTime dataPrazo, string? observacoes)
        {
            ValidarCamposHistorico(usuarioId, titulo, status, dataPrazo);

            Titulo = titulo;
            Status = status;
            DataPrazo = dataPrazo;
            Observacoes = observacoes;
        }

        private void ValidarCamposHistorico(Guid usuarioId, string novoTitulo, Status novoStatus, DateTime novoDataPrazo)
        {
            if (Titulo != novoTitulo)
                AdicionarHistorico(usuarioId, $"Título alterado de {Titulo} para {novoTitulo}");

            if (Status != novoStatus)
                AdicionarHistorico(usuarioId, $"Status alterado de {Status} para {novoStatus}");

            if (DataPrazo != novoDataPrazo)
                AdicionarHistorico(usuarioId, $"Status alterado de {DataPrazo} para {novoDataPrazo}");
        }

        public void AdicionarHistorico(Guid usuarioId, string descricao)
        {
            Historico.Add(new Historico(usuarioId, descricao, this));
        }
    }
}
