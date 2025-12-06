using HippoOrders.Component;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HippoOrders.Forms
{
    public partial class FormListGods : Form
    {
        // 產品點擊事件
        public event EventHandler<ProductClickEventArgs> ProductClickEvent;
        // 產品游標樣式
        public Cursor ProductCursor { get; set; } = Cursors.Default;

        public FormListGods()
        {
            InitializeComponent();
        }

        private void FormListGods_Load(object sender, EventArgs e)
        {
            LoadProductsFromDb();
        }

        private void LoadProductsFromDb()
        {
            godsItemsBox.Controls.Clear();

            try
            {
                // 從資料庫讀取產品資料
                using (SqlConnection conn = new SqlConnection(DbInitializer.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("SELECT id, name, price, image FROM goods", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var component = CreateProductShowcase(reader);
                            godsItemsBox.Controls.Add(component);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取資料失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BasicProductShowcaseComponent CreateProductShowcase(SqlDataReader reader)
        {
            // 建立產品展示元件
            var component = new BasicProductShowcaseComponent
            {
                Cursor = ProductCursor,
                ProductID = reader.GetInt32(reader.GetOrdinal("id")),
                ProductName = reader["name"].ToString(),
                ProductPrice = reader.GetDecimal(reader.GetOrdinal("price")),
                Tag = reader.GetInt32(reader.GetOrdinal("id"))
            };

            // 設定產品圖片
            if (reader["image"] != DBNull.Value)
            {
                byte[] imgBytes = (byte[])reader["image"];
                component.SetImageFromBytes(imgBytes);
            }

            var newComponent = component;
            // 綁定點擊事件
            component.Click += (s, e) =>
            {
                Control clickedControl = s as Control;
                BasicProductShowcaseComponent targetItem = null;

                // 往上尋找 BasicProductShowcaseComponent
                while (clickedControl != null)
                {
                    if (clickedControl is BasicProductShowcaseComponent bpsc)
                    {
                        targetItem = bpsc;
                        break;
                    }
                    clickedControl = clickedControl.Parent;
                }

                // 觸發產品點擊事件
                ProductClickEvent?.Invoke(
                    clickedControl,
                    new ProductClickEventArgs(newComponent.ProductID, newComponent)
                );
            };

            return component;
        }

        // 產品點擊事件參數
        public class ProductClickEventArgs : EventArgs
        {
            public int ProductID { get; }
            public ProductShowcaseBase ProductShowcase { get; }

            public ProductClickEventArgs(int productID, ProductShowcaseBase productShowcaseBase)
            {
                ProductID = productID;
                ProductShowcase = productShowcaseBase;
            }
        }
    }
}