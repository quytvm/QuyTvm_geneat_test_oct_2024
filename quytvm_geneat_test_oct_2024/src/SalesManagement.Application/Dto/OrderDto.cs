using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public ICollection<ProductOrderDto> ProductOrders { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
