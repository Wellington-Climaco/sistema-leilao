using FluentValidation;
using SistemaLeilao.Application.Request.Bem;

namespace SistemaLeilao.Application.Validators.Bem;

public class CreateBemValidator : AbstractValidator<CreateBemRequest>
{
    public CreateBemValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo nome não pode ser nulo")
            .MinimumLength(3).WithMessage("Campo nome deve ser maior que 3 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Campo descricao não pode ser nulo");
        
        RuleFor(x=>x.ValorMinimo)
            .GreaterThan(0).WithMessage("Campo valor minimo deve ser maior que 0");
    }
}