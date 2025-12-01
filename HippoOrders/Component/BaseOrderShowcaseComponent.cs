using HippoOrders.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HippoOrders.Component
{
    public partial class BaseOrderShowcaseComponent : OrderShowcaseBase
    {
        private Image _arrowBase;

        public BaseOrderShowcaseComponent()
        {
            InitializeComponent();
        }

        private void BaseOrderShowcaseComponent_Load(object sender, EventArgs e)
        {
            _arrowBase = (Image)showListBtn.Image.Clone();
            userNameLab.Text = Order.Name;
            infoTextLab.Text = $"{Order.Address} [{FormatPhone(Order.Phone)}]";
            LoadOrderItems();
        }

        private void ShowListBtn_Click(object sender, EventArgs e)
        {
            orderGodListBox.Visible = !orderGodListBox.Visible;

            if (orderGodListBox.Visible)
            {
                showListBtn.Image = (Image)_arrowBase.Clone();
            }
            else
            {
                var img = (Image)_arrowBase.Clone();
                img.RotateFlip(RotateFlipType.Rotate90FlipX);
                showListBtn.Image = img;
            }
        }

        private void LoadOrderItems()
        {
            orderGodListBox.Controls.Clear();

            List<OrderItemDetailModel> items = GetItemsFromDatabase(this.Order.ID);

            foreach (var item in items)
            {
                var productControl = new CartProductShowcaseComponent
                {
                    Dock = DockStyle.Top,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                };

                if (item.ImageData != null && item.ImageData.Length > 0)
                {
                    using (var ms = new MemoryStream(item.ImageData))
                    {
                        productControl.ProductImage = Image.FromStream(ms);
                    }
                }

                productControl.DisableQuantityEditing();

                orderGodListBox.Controls.Add(productControl);
            }
        }

        private List<OrderItemDetailModel> GetItemsFromDatabase(int orderID)
        {
            var list = new List<OrderItemDetailModel>();
            string sql = @"
                SELECT g.name, g.price, g.image, i.quantity
                FROM items i
                JOIN goods g ON i.product_id = g.id
                WHERE i.order_id = @oid";

            using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@oid", orderID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new OrderItemDetailModel
                            {
                                ProductName = reader["name"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                ImageData = reader["image"] as byte[]
                            });
                        }
                    }
                }
            }
            return list;
        }

        string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;

            string digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length != 10) return phone;

            return $"{digits.Substring(0, 4)}-{digits.Substring(4, 3)}-{digits.Substring(7, 3)}";
        }
    }
}
