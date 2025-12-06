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
        // 購物車商品字典
        private readonly Dictionary<int, CartProductShowcaseComponent> Carts = new Dictionary<int, CartProductShowcaseComponent>();
        // 商品列表表單
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

            // 訂閱商品點擊事件
            GodsForm.ProductClickEvent += OnProductClick;
            // 將商品列表表單添加到 godsBox 控制項中
            godsBox.Controls.Add(GodsForm);
            // 顯示商品列表表單
            GodsForm.Show();
        }

        private void OnProductClick(object targetItem, ProductClickEventArgs productClickEventArgs)
        {
            // 處理商品點擊事件，將商品添加到購物車
            if (!(targetItem is BasicProductShowcaseComponent targetItemComponent)) return;

            ProductShowcaseBase productShowcaseBase = targetItemComponent;
            // 如果商品已存在於購物車中，增加數量
            if (Carts.ContainsKey(productClickEventArgs.ProductID))
            {
                var existingCartItem = Carts[productShowcaseBase.ProductID];
                existingCartItem.Quantity += 1;
                // 使滾動條滾動到該商品位置
                cartItemsBox.ScrollControlIntoView(existingCartItem);
                return;
            }

            // 否則，創建新的購物車商品元件並添加到購物車
            var cartItem = new CartProductShowcaseComponent
            {
                ProductID = productShowcaseBase.ProductID,
                ProductName = productShowcaseBase.ProductName,
                ProductPrice = productShowcaseBase.ProductPrice,
                ProductImage = productShowcaseBase.ProductImage,
                Dock = DockStyle.Top
            };
            // 訂閱數量變更事件
            cartItem.QuantityChanged += CartItem_QuantityChanged;

            // 將新商品添加到購物車字典
            Carts.Add(cartItem.ProductID, cartItem);
            UpdateGrandTotal();

            // 將新商品元件添加到購物車顯示區域
            cartItemsBox.Controls.Add(cartItem);
            // 移動滾動條以顯示新添加的商品
            cartItemsBox.ScrollControlIntoView(cartItem);
        }

        private void CartItem_QuantityChanged(object sender, QuantityChangedEventArgs e)
        {
            // 處理購物車商品數量變更事件 (移除商品)
            if (sender is CartProductShowcaseComponent cartItem && e.Quantity <= 0)
            {
                // 確認是否移除商品
                DialogResult dialogResult = MessageBox.Show("確定要從購物車移除此商品嗎？", "確認移除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // 如果用戶選擇不移除，恢復原來的數量
                if (dialogResult != DialogResult.Yes && e.OldQuantity > 0)
                {
                    cartItem.Quantity = e.OldQuantity > 0 ? e.OldQuantity : 1;
                    return;
                }

                // 從購物車字典和顯示區域中移除商品
                Carts.Remove(cartItem.ProductID);
                cartItemsBox.Controls.Remove(cartItem);
            }

            // 更新總價
            UpdateGrandTotal();
        }

        private void UpdateGrandTotal()
        {
            // 計算並更新購物車總價
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

            // 取得用戶輸入的訂單資訊
            string address = addInpBox.Text.Trim();
            string phone = phoneInpBox.Text.Trim();
            string name = nameInpBox.Text.Trim();
            // 檢查必要資訊是否完整
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("請輸入完整的「電話」與「名字」！", "資料缺漏", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 計算最終總價
            decimal finalTotalPrice = Carts.Values.Sum(c => c.ProductPrice * c.Quantity);

            using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 插入訂單資料
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

                        // 取得新插入的訂單 ID
                        newOrderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 插入訂單項目資料
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
