using HippoOrders.Component;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace HippoOrders.Forms
{
    public partial class FormAddOrder : Form
    {
        Dictionary<int, CartProductShowcaseComponent> carts = new Dictionary<int, CartProductShowcaseComponent>();

        public FormAddOrder()
        {
            InitializeComponent();
        }

        private void FormAddOrder_Load(object sender, EventArgs e)
        {
            LoadProductsFromDb();
        }

        private void LoadProductsFromDb()
        {
            godsItemsBox.Controls.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, name, price, image FROM goods", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new BasicProductShowcaseComponent
                                {
                                    ProductID = (int)reader["id"],
                                    ProductName = reader["name"].ToString(),
                                    ProductPrice = (decimal)reader["price"],
                                    Cursor = Cursors.Hand,
                                    Tag = (int)reader["id"]
                                };

                                if (reader["image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["image"];
                                    item.SetImageFromBytes(imgBytes);
                                }

                                item.Click += OnProductClick;
                                godsItemsBox.Controls.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取資料失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnProductClick(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            BasicProductShowcaseComponent targetItem = null;

            while (clickedControl != null)
            {
                if (clickedControl is BasicProductShowcaseComponent item)
                {
                    targetItem = item;
                    break;
                }
                clickedControl = clickedControl.Parent;
            }

            if (targetItem == null) return;

            if (carts.ContainsKey(targetItem.ProductID))
            {
                var existingCartItem = carts[targetItem.ProductID];
                existingCartItem.Quantity += 1;
                cartItemsBox.ScrollControlIntoView(existingCartItem);
                return;
            }

            var cartItem = new CartProductShowcaseComponent
            {
                ProductID = targetItem.ProductID,
                ProductName = targetItem.ProductName,
                ProductPrice = targetItem.ProductPrice,
                ProductImage = targetItem.ProductImage,
                Dock = DockStyle.Top
            };
            cartItem.QuantityChanged += CartItem_QuantityChanged;

            carts.Add(cartItem.ProductID, cartItem);
            UpdateGrandTotal();

            cartItemsBox.Controls.Add(cartItem);
            cartItemsBox.ScrollControlIntoView(cartItem);
        }

        private void CartItem_QuantityChanged(object sender, QuantityChangedEventArgs e)
        {
            UpdateGrandTotal();
        }

        private void UpdateGrandTotal()
        {
            decimal total = carts.Values.Sum(item => item.ProductPrice * item.Quantity);

            totalPriceBox.Text = $"{total:N0}";
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (carts.Count == 0)
            {
                MessageBox.Show("購物車是空的，無法結帳！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string address = addInpBox.Text.Trim();
            string phone = phoneInpBox.Text.Trim();
            string name = nameInpBox.Text.Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("請輸入完整的「電話」與「名字」！", "資料缺漏", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal finalTotalPrice = carts.Values.Sum(c => c.ProductPrice * c.Quantity);

            using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string insertOrderSql = @"
                        INSERT INTO [orders] (name, address, phone, total_price) 
                        VALUES (@name, @address, @phone, @total_price);
                        SELECT SCOPE_IDENTITY();";

                    int newOrderId = 0;

                    using (SqlCommand cmd = new SqlCommand(insertOrderSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@total_price", finalTotalPrice);

                        newOrderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string insertItemSql = @"
                        INSERT INTO [items] (order_id, product_id, quantity) 
                        VALUES (@order_id, @product_id, @quantity);";

                    foreach (var cartItem in carts.Values)
                    {
                        using (SqlCommand cmd = new SqlCommand(insertItemSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@order_id", newOrderId);
                            cmd.Parameters.AddWithValue("@product_id", cartItem.ProductID);
                            cmd.Parameters.AddWithValue("@quantity", cartItem.Quantity);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    MessageBox.Show($"訂單送出成功！\n訂單編號：{newOrderId}\n金額：${finalTotalPrice:N0}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearCart();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"訂單建立失敗：{ex.Message}", "系統錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearCart()
        {
            cartItemsBox.Controls.Clear();
            carts.Clear();
            UpdateGrandTotal();
        }
    }
}
