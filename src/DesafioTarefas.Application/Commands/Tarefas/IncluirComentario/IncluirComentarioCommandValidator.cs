using FluentValidation;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirComentario
{
    public class IncluirComentarioCommandValidator : AbstractValidator<IncluirComentarioCommand>
    {
        public IncluirComentarioCommandValidator()
        {
            RuleFor(c => c.Texto)
                .NotEmpty()
                .WithMessage("O texto é obrigatório");

            RuleFor(c => c.Texto)
                .MinimumLength(10)
                .When(c => !string.IsNullOrEmpty(c.Texto))
                .WithMessage("O texto deve conter no mímimo 10 caracteres");
        }
    }
}