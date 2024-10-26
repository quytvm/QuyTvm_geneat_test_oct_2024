using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Dto
{
    public class ProdcutSearch
    {
        public string? searchString { get; set; }
        public string? sortOrder { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
