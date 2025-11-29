using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace HippoOrders
{
    public class DbInitializer
    {
        private static string DbFolder => Path.Combine(Application.StartupPath, "db");
        private static string DbFile => Path.Combine(DbFolder, "db.mdf");
        private static string LogFile => Path.Combine(DbFolder, "db_log.ldf");

        private readonly static string MasterConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        public static string GetConnectionString()
        {
            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbFile};Integrated Security=True;Connect Timeout=30";
        }

        public static void Initialize()
        {
            try
            {
                if (!Directory.Exists(DbFolder))
                {
                    Directory.CreateDirectory(DbFolder);
                }

                if (!File.Exists(DbFile))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    CreateDatabase();
                    CreateTables();
                    SeedData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化資料庫失敗: {ex.Message}", "系統錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDatabase()
        {
            using (var conn = new SqlConnection(MasterConnStr))
            {
                conn.Open();
                string sql = $@"
                    CREATE DATABASE [HippoOrdersDB]
                    ON PRIMARY (NAME=HippoOrders_Data, FILENAME = '{DbFile}')
                    LOG ON (NAME=HippoOrders_Log, FILENAME = '{LogFile}')";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void CreateTables()
        {
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE [dbo].[orders] (
                        [id]          INT             IDENTITY(1,1) NOT NULL,
                        [name]        NVARCHAR (100)  NOT NULL,
                        [address]     NVARCHAR (255)  NOT NULL,
                        [phone]       NVARCHAR (10)   NOT NULL,
                        [total_price] DECIMAL (10, 2) NOT NULL,
                        [order_date]  DATETIME        DEFAULT (getdate()) NOT NULL,
                        PRIMARY KEY CLUSTERED ([id] ASC)
                    );

                    CREATE TABLE [dbo].[goods] (
                        [id]    INT             IDENTITY(1,1) NOT NULL,
                        [name]  NVARCHAR (100)  NOT NULL,
                        [price] DECIMAL (10, 2) NOT NULL,
                        [image] VARBINARY (MAX) NULL,
                        PRIMARY KEY CLUSTERED ([id] ASC)
                    );

                    CREATE TABLE [dbo].[items] (
                        [order_id]   INT NOT NULL,
                        [product_id] INT NOT NULL,
                        [quantity]   INT NOT NULL,
                        PRIMARY KEY CLUSTERED ([order_id] ASC, [product_id] ASC),
                        CONSTRAINT [FK_items_orders] FOREIGN KEY ([order_id]) REFERENCES [dbo].[orders] ([id]) ON DELETE CASCADE,
                        CONSTRAINT [FK_items_goods] FOREIGN KEY ([product_id]) REFERENCES [dbo].[goods] ([id]) ON DELETE CASCADE
                    );
                ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void SeedData()
        {
            var products = new[]
            {
                new { Name = "招牌牛肉麵", Price = 120, Url = "https://placehold.co/300x200/png?text=Beef+Noodle" },
                new { Name = "珍珠奶茶", Price = 60, Url = "https://placehold.co/300x200/png?text=Bubble+Tea" },
                new { Name = "古早味滷肉飯", Price = 50, Url = "https://placehold.co/300x200/png?text=Rice" }
            };

            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                foreach (var p in products)
                {
                    byte[] imgBytes = GetImageFromUrl(p.Url);
                    string sql = "INSERT INTO [dbo].[goods] ([name], [price], [image]) VALUES (@name, @price, @image)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", p.Name);
                        cmd.Parameters.AddWithValue("@price", p.Price);

                        if (imgBytes != null) cmd.Parameters.AddWithValue("@image", imgBytes);
                        else cmd.Parameters.AddWithValue("@image", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static byte[] GetImageFromUrl(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    return client.GetByteArrayAsync(url).Result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
