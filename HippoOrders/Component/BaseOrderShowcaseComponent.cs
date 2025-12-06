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
        // 箭頭圖片
        private Image _arrowBase;

        public BaseOrderShowcaseComponent()
        {
            InitializeComponent();
        }

        private void BaseOrderShowcaseComponent_Load(object sender, EventArgs e)
        {
            // 初始化箭頭圖片
            _arrowBase = (Image)showListBtn.Image.Clone();
            // 設置訂單基本資訊
            userNameLab.Text = Order.Name;
            // 格式化地址和電話資訊
            infoTextLab.Text = $"{Order.Address} [{FormatPhone(Order.Phone)}]";
            // 設置訂單總價
            LoadOrderItems();
        }

        private void ShowListBtn_Click(object sender, EventArgs e)
        {
            // 切換訂單商品列表的可見性
            orderGodListBox.Visible = !orderGodListBox.Visible;

            // 更新按鈕圖片方向
            if (orderGodListBox.Visible)
            {
                showListBtn.Image = (Image)_arrowBase.Clone();
            }
            else
            {
                // 旋轉箭頭圖片以指示折疊狀態
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
                // 為每個訂單項目創建並配置商品展示控制項
                var productControl = new CartProductShowcaseComponent
                {
                    Dock = DockStyle.Top,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                };

                if (item.ImageData != null && item.ImageData.Length > 0)
                {
                    // 讀取圖片數據並設置商品圖片 (byte[] 轉 Image)
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
            // SQL 查詢以獲取訂單項目詳情
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

            // 提取數字部分
            string digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length != 10) return phone;

            // 格式化為 0000-000-000 形式
            return $"{digits.Substring(0, 4)}-{digits.Substring(4, 3)}-{digits.Substring(7, 3)}";
        }
    }
}
