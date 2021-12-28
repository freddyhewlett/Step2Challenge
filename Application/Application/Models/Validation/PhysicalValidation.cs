using Domain.Models.Suppliers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Validation
{
    public class PhysicalValidation : AbstractValidator<SupplierPhysical>
    {
        public PhysicalValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Nome completo obrigatório.")
                .Length(4, 120)
                .WithMessage("Nome completo deve ter entre 4 e 120 caracteres.");

            RuleFor(x => x.Cpf)
                .Length(11)
                .WithMessage("CPF deve conter 11 numeros.");

            RuleFor(x => x.BirthDate.Year)
                .LessThanOrEqualTo(DateTime.Now.Year - 18)
                .WithMessage("Cadastro permitido apenas para maiores de 18 anos.");
        }
    }
}
