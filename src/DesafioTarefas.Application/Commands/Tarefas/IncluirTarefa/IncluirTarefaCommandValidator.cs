using FluentValidation;

namespace DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa
{
    public class IncluirTarefaCommandValidator : AbstractValidator<IncluirTarefaCommand>
    {
        public IncluirTarefaCommandValidator()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Titulo)
                .MaximumLength(255)
                .When(c => !string.IsNullOrEmpty(c.Titulo))
                .WithMessage("O título deve conter no máximo 255 caracteres");
        }
    }
}