using Domain.Models.Suppliers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Validation
{
    public class JuridicalValidation : AbstractValidator<SupplierJuridical>
    {
        public JuridicalValidation()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Nome real da empresa é obrigatório.")
                .Length(2, 160)
                .WithMessage("Nome da empresa deve ter entre 2 e 160 caracteres.");

            RuleFor(x => x.Cnpj)
                .Length(14)
                .WithMessage("CNPJ deve conter 14 numeros.");
        }
    }
}
