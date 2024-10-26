using FluentValidation;
using SalesManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName)
                .NotNull().NotEmpty().WithMessage("Product name is required.")
                .Length(2, 50).WithMessage("Product name must be between 2 and 50 characters.");

            RuleFor(p => p.PurchasePrice)
                .NotEmpty().WithMessage("PurchasePrice is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.SalePrice)
                .NotEmpty().WithMessage("SalePrice is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.TaxRate)
                .NotEmpty().WithMessage("TaxRate is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.Unit)
                .NotEmpty().WithMessage("Unit is required.")
                .NotEmpty().WithMessage("Unit is required.");
        }
    }
}
