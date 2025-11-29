using HippoOrders.Component;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HippoOrders.Forms
{
    public partial class FormListGods : Form
    {
        public event EventHandler<ProductClickEventArgs> ProductClickEvent;
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
            var component = new BasicProductShowcaseComponent
            {
                Cursor = ProductCursor,
                ProductID = reader.GetInt32(reader.GetOrdinal("id")),
                ProductName = reader["name"].ToString(),
                ProductPrice = reader.GetDecimal(reader.GetOrdinal("price")),
                Tag = reader.GetInt32(reader.GetOrdinal("id"))
            };

            if (reader["image"] != DBNull.Value)
            {
                byte[] imgBytes = (byte[])reader["image"];
                component.SetImageFromBytes(imgBytes);
            }

            var newComponent = component;
            component.Click += (s, e) =>
            {
                Control clickedControl = s as Control;
                BasicProductShowcaseComponent targetItem = null;

                while (clickedControl != null)
                {
                    if (clickedControl is BasicProductShowcaseComponent bpsc)
                    {
                        targetItem = bpsc;
                        break;
                    }
                    clickedControl = clickedControl.Parent;
                }

                ProductClickEvent?.Invoke(
                    clickedControl,
                    new ProductClickEventArgs(newComponent.ProductID, newComponent)
                );
            };

            return component;
        }

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