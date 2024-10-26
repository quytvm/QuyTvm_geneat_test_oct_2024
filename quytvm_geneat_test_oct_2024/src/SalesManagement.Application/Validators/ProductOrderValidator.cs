using FluentValidation;
using SalesManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Validators
{
    public class ProductOrderValidator : AbstractValidator<CreateProductOrderDto>
    {
        public ProductOrderValidator() {
            RuleFor(p => p.ProductId)
                .NotNull().NotEmpty().WithMessage("Product is required.");

            RuleFor(p => p.Quantity)
                .NotNull().NotEmpty().WithMessage("Quantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(p => p.UnitPrice)
                .NotNull().NotEmpty().WithMessage("UnitPrice is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.TaxRate)
                .NotNull().NotEmpty().WithMessage("TaxRate is required.")
                .NotNull().WithMessage("TaxRate is required.")
                .GreaterThanOrEqualTo(0).WithMessage("TaxRate must be greater than 0.");
        }
    }
}
