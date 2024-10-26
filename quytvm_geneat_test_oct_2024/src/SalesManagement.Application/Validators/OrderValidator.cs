using FluentValidation;
using SalesManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Validators
{
    public class OrderValidator : AbstractValidator<CreateOrderDto>
    {
        public OrderValidator() {
            RuleFor(p => p.CustomerName)
                .NotNull().NotEmpty().WithMessage("Customer name is required.")
                .Length(2, 100).WithMessage("Customer name must be between 2 and 100 characters.");

            RuleFor(p => p.CustomerPhone)
                 .NotNull().NotEmpty().WithMessage("Customer phone is required.")
                 .Length(6, 20).WithMessage("Customer phone must be between 6 and 20 characters.");

            RuleFor(x => x.productOrders)
            .NotEmpty().WithMessage("Đơn hàng phải có ít nhất một sản phẩm");

            RuleForEach(x => x.productOrders)
                .SetValidator(new ProductOrderValidator());
        }
    }
}
