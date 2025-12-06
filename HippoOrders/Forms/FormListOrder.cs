using HippoOrders.Component;
using HippoOrders.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            LoadOrdersFromDb(
                searchInp.Text.Trim(),
                null,
                null,
                null,
                null
            );
        }

        // 從資料庫加載訂單並根據條件篩選
        public void LoadOrdersFromDb(
            string keyword,
            DateTime? startDate,
            DateTime? endDate,
            decimal? minPrice,
            decimal? maxPrice)
        {
            ordersBox.Controls.Clear();

            List<OrderModel> orders = GetOrdersFromDatabase(
                keyword,
                startDate,
                endDate,
                minPrice,
                maxPrice
            );

            foreach (OrderModel order in orders)
            {
                BaseOrderShowcaseComponent orderComponent = new BaseOrderShowcaseComponent();
                orderComponent.Order = order;
                orderComponent.Dock = DockStyle.Top;
                ordersBox.Controls.Add(orderComponent);
            }
        }

        // 從資料庫中獲取訂單
        private List<OrderModel> GetOrdersFromDatabase(
            string keyword,
            DateTime? startDate,
            DateTime? endDate,
            decimal? minPrice,
            decimal? maxPrice)
        {
            List<OrderModel> list = new List<OrderModel>();

            string sql = @"
                SELECT id, name, address, phone, total_price, order_date
                FROM orders
                WHERE
                    (@kw = '' OR 
                        name LIKE '%' + @kw + '%' OR
                        phone LIKE '%' + @kw + '%' OR
                        address LIKE '%' + @kw + '%')
                AND (@start IS NULL OR order_date >= @start)
                AND (@end IS NULL OR order_date <= @end)
                AND (@min IS NULL OR total_price >= @min)
                AND (@max IS NULL OR total_price <= @max)
                ORDER BY order_date DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@kw", keyword ?? "");

                        if (startDate.HasValue) cmd.Parameters.AddWithValue("@start", startDate.Value);
                        else cmd.Parameters.AddWithValue("@start", DBNull.Value);

                        if (endDate.HasValue) cmd.Parameters.AddWithValue("@end", endDate.Value);
                        else cmd.Parameters.AddWithValue("@end", DBNull.Value);

                        if (minPrice.HasValue) cmd.Parameters.AddWithValue("@min", minPrice.Value);
                        else cmd.Parameters.AddWithValue("@min", DBNull.Value);

                        if (maxPrice.HasValue) cmd.Parameters.AddWithValue("@max", maxPrice.Value);
                        else cmd.Parameters.AddWithValue("@max", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderModel model = new OrderModel
                                {
                                    ID = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Address = reader["address"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    TotalPrice = (decimal)reader["total_price"],
                                    OrderDate = (DateTime)reader["order_date"]
                                };

                                list.Add(model);
                            }
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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            LoadOrdersFromDb();
        }
    }
}
