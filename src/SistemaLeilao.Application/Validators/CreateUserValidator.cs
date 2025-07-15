using FluentValidation;
using SistemaLeilao.Application.Request;

namespace SistemaLeilao.Application.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.nome)
            .NotEmpty().WithMessage("Campo nome não pode ser nulo")
            .MinimumLength(3).WithMessage("Campo nome deve ser maior que 3 caracteres");
        
        RuleFor(x => x.email)
            .NotEmpty().WithMessage("Campo email não pode ser nulo")
            .EmailAddress().WithMessage("campo email inválido");
    }
}