using Domain.Models.Suppliers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Validation
{
    public class PhoneValidation : AbstractValidator<Phone>
    {
        public PhoneValidation()
        {
            RuleFor(x => x.Ddd)
                   .NotEmpty()
                   .WithMessage("O ddd é obrigatorio")
                   .Length(2, 2)
                   .WithMessage("o ddd deve conter 2 numeros");

            When(x => x.PhoneType == Enum.PhoneType.Mobile, () =>
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O celular é obrigatorio")
                   .Length(9, 9)
                   .WithMessage("o celular deve conter 9 numeros");
            });

            When(x => x.PhoneType == Enum.PhoneType.Home, () =>
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O telefone fixo é obrigatorio")
                   .Length(8, 8)
                   .WithMessage("o telefone fixo deve conter 8 numeros");
            });

            When(x => x.PhoneType == Enum.PhoneType.Office, () =>
            {
                RuleFor(x => x.Number)
                   .NotEmpty()
                   .WithMessage("O telefone comercial é obrigatorio")
                   .Length(8, 9)
                   .WithMessage("o telefone comercial deve conter entre 8 e 9 numeros");
            });


        }
    }
}
