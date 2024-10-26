using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Dto
{
    public class CreateOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public ICollection<CreateProductOrderDto> productOrders { get; set; }

    }
}
