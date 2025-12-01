using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HippoOrders.Models
{
    public class OrderItemDetailModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public byte[] ImageData { get; set; }
    }
}
