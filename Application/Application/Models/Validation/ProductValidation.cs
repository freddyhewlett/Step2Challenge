using Domain.Models.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Validation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            //RuleFor(x => x.)
        }
    }
}
