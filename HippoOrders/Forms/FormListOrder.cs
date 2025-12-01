using HippoOrders.Component;
using HippoOrders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HippoOrders.Forms
{
    public partial class FormListOrder : Form
    {
        public FormListOrder()
        {
            InitializeComponent();
        }

        private void FormListOrder_Load(object sender, EventArgs e)
        {
            LoadOrdersFromDb();
            
        }

        public void LoadOrdersFromDb()
        {
            ordersBox.Controls.Clear();

            List<OrderModel> orders = GetOrdersFromDatabase();

            foreach (var order in orders)
            {
                var orderComponent = new BaseOrderShowcaseComponent
                {
                    Order = order,
                    Dock = DockStyle.Top,
                };
                ordersBox.Controls.Add(orderComponent);
            }
        }

        private List<OrderModel> GetOrdersFromDatabase()
        {
            var list = new List<OrderModel>();

            string sql = "SELECT id, name, address, phone, total_price, order_date FROM orders ORDER BY order_date DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new OrderModel
                            {
                                ID = (int)reader["id"],
                                Name = reader["name"].ToString(),
                                Address = reader["address"].ToString(),
                                Phone = reader["phone"].ToString(),
                                TotalPrice = (decimal)reader["total_price"],
                                OrderDate = (DateTime)reader["order_date"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取訂單失敗: " + ex.Message);
            }

            return list;
        }
    }
}
