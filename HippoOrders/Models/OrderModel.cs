using System;

namespace HippoOrders.Models
{
    // 訂單資料
    public class OrderModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
