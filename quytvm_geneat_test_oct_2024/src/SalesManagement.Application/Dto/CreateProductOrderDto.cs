using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Dto
{
    public class CreateProductOrderDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDto ProductDto { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
