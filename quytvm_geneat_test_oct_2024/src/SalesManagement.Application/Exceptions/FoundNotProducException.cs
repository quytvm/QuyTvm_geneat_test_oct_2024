using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Exceptions
{
    public class FoundNotProducException : Exception
    {
        public FoundNotProducException(string message)
        : base(message) { }

        public FoundNotProducException(string message, Exception innerException)
            : base(message, innerException) { }

        public int ProductId { get; set; }

        public FoundNotProducException(int productId)
            : base($"Product with ID {productId} does not exist.")
        {
            ProductId = productId;
        }
    }
}
