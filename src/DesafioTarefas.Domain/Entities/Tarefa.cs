using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; private set; }

        public string Titulo { get; private set; }

        public Prioridade Prioridade { get; private set; }

        public Status Status { get; private set; }

        public DateOnly DataPrazo { get; private set; }

        public string? Observacoes { get; private set; }

        public DateOnly? DataConclusao { get; private set; }

        public Projeto Projeto { get; private set; } = null!;

        public bool Concluida => Status == Status.Concluida;

        public ICollection<Historico> Historico { get; private set; }

        public ICollection<Comentario> Comentarios { get; private set; }

        private Tarefa()
        {
            Titulo = string.Empty;
            Historico = new HashSet<Historico>();
            Comentarios = new HashSet<Comentario>();
        }

        public Tarefa(string titulo, Prioridade prioridade, DateOnly dataPrazo, string? observacoes)
            : this(
                Guid.NewGuid(),
                titulo,
                prioridade,
                Status.Pendente,
                dataPrazo,
                observacoes)
        {
        }

        public Tarefa(Guid id, string titulo, Prioridade prioridade, Status status, DateOnly dataPrazo, string? observacoes)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            Status = status;
            DataPrazo = dataPrazo;
            Observacoes = observacoes;

            Historico = new HashSet<Historico>();
            Comentarios = new HashSet<Comentario>();
        }

        public static Tarefa CriarTarefaParaTeste(Status status = Status.Pendente) =>
            new Tarefa(
                Guid.NewGuid(),
                "Tarefa X",
                Prioridade.Media,
                status,
                DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                string.Empty);

        public void Atualizar(Guid usuarioId, string titulo, Status status, DateOnly dataPrazo, string? observacoes)
        {
            ValidarCamposHistorico(usuarioId, titulo, status, dataPrazo);

            SetarDataConclusao(status);

            Titulo = titulo;
            Status = status;
            DataPrazo = dataPrazo;
            Observacoes = observacoes;
        }

        private void SetarDataConclusao(Status status)
        {
            if (Status == status)
                return;

            if (status == Status.Concluida)
                DataConclusao = DateOnly.FromDateTime(DateTime.Now);

            if (status != Status.Concluida)
                DataConclusao = null;
        }

        private void ValidarCamposHistorico(Guid usuarioId, string novoTitulo, Status novoStatus, DateOnly novoDataPrazo)
        {
            if (Titulo != novoTitulo)
                AdicionarHistorico(usuarioId, nameof(Titulo), Titulo, novoTitulo);

            if (Status != novoStatus)
                AdicionarHistorico(usuarioId, nameof(Status), Status.ToString(), novoStatus.ToString());

            if (DataPrazo != novoDataPrazo)
                AdicionarHistorico(usuarioId, nameof(DataPrazo), DataPrazo.ToString(), novoDataPrazo.ToString());
        }

        private void AdicionarHistorico(Guid usuarioId, string campo, string? valorOriginal, string? novoValor)
        {
            Historico.Add(new Historico(usuarioId, campo, valorOriginal, novoValor, this));
        }

        public void AdicionarComentario(Guid usuarioId, string texto)
        {
            Comentarios.Add(new Comentario(usuarioId, texto, this));

            AdicionarHistorico(usuarioId, "Comentario", null, null);
        }
    }
}
