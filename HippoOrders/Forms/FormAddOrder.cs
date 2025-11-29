using HippoOrders.Component;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static HippoOrders.Forms.FormListGods;

namespace HippoOrders.Forms
{
    public partial class FormAddOrder : Form
    {
        private readonly Dictionary<int, CartProductShowcaseComponent> Carts = new Dictionary<int, CartProductShowcaseComponent>();
        private readonly FormListGods GodsForm = new FormListGods
        {
            TopLevel = false,
            Dock = DockStyle.Fill,
            ProductCursor = Cursors.Hand,
            FormBorderStyle = FormBorderStyle.None,
        };

        public FormAddOrder()
        {
            InitializeComponent();

            GodsForm.ProductClickEvent += OnProductClick;
            godsBox.Controls.Add(GodsForm);
            GodsForm.Show();
        }

        private void OnProductClick(object targetItem, ProductClickEventArgs productClickEventArgs)
        {
            if (!(targetItem is BasicProductShowcaseComponent targetItemComponent)) return;
            ProductShowcaseBase productShowcaseBase = targetItemComponent;

            if (Carts.ContainsKey(productClickEventArgs.ProductID))
            {
                var existingCartItem = Carts[productShowcaseBase.ProductID];
                existingCartItem.Quantity += 1;
                cartItemsBox.ScrollControlIntoView(existingCartItem);
                return;
            }

            var cartItem = new CartProductShowcaseComponent
            {
                ProductID = productShowcaseBase.ProductID,
                ProductName = productShowcaseBase.ProductName,
                ProductPrice = productShowcaseBase.ProductPrice,
                ProductImage = productShowcaseBase.ProductImage,
                Dock = DockStyle.Top
            };
            cartItem.QuantityChanged += CartItem_QuantityChanged;

            Carts.Add(cartItem.ProductID, cartItem);
            UpdateGrandTotal();

            cartItemsBox.Controls.Add(cartItem);
            cartItemsBox.ScrollControlIntoView(cartItem);
        }

        private void CartItem_QuantityChanged(object sender, QuantityChangedEventArgs e)
        {
            if (sender is CartProductShowcaseComponent cartItem && e.Quantity <= 0)
            {
                DialogResult dialogResult = MessageBox.Show("確定要從購物車移除此商品嗎？", "確認移除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes && e.OldQuantity > 0)
                {
                    cartItem.Quantity = e.OldQuantity > 0 ? e.OldQuantity : 1;
                    return;
                }

                Carts.Remove(cartItem.ProductID);
                cartItemsBox.Controls.Remove(cartItem);
            }

            UpdateGrandTotal();
        }

        private void UpdateGrandTotal()
        {
            decimal total = Carts.Values.Sum(item => item.ProductPrice * item.Quantity);

            totalPriceBox.Text = $"{total:N0}";
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (Carts.Count == 0)
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

            decimal finalTotalPrice = Carts.Values.Sum(c => c.ProductPrice * c.Quantity);

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

                    foreach (var cartItem in Carts.Values)
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
            Carts.Clear();
            UpdateGrandTotal();
        }
    }
}
