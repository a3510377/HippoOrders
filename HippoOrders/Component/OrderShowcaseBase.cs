using HippoOrders.Models;
using System.Windows.Forms;

namespace HippoOrders.Component
{
    // 訂單展示基類
    public class OrderShowcaseBase : UserControl
    {
        public OrderModel Order { get; set; }
    }
}
