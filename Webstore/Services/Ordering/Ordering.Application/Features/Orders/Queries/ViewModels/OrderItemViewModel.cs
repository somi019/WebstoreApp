using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.ViewModels
{
    public class OrderItemViewModel
    {
        // relevant info from EntityBase
        public int Id { get; set; }
        // relevant info from OrderItem
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
    }
}
// ViewModel zapravo podrazumeva DataClass, kao sto smo radili kod Boraka za Kotlin
