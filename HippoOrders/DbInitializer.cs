using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace HippoOrders
{
    public class DbInitializer
    {
        // 資料庫資料夾
        private static string DbFolder => Path.Combine(Application.StartupPath, "db");
        // 資料庫檔案路徑
        private static string DbFile => Path.Combine(DbFolder, "db.mdf");
        // 日誌檔案路徑
        private static string LogFile => Path.Combine(DbFolder, "db_log.ldf");

        // 主資料庫連接字串
        private readonly static string MasterConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        // 取得應用程式的資料庫連接字串
        public static string GetConnectionString()
        {
            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbFile};Integrated Security=True;Connect Timeout=30";
        }

        public static void Initialize()
        {
            try
            {
                // 確保資料庫資料夾存在
                if (!Directory.Exists(DbFolder))
                {
                    Directory.CreateDirectory(DbFolder);
                }

                // 如果資料庫檔案不存在，則建立資料庫和表格，並插入初始資料
                if (!File.Exists(DbFile))
                {
                    // 將游標設為等待游標
                    Cursor.Current = Cursors.WaitCursor;
                    // 建立資料庫
                    CreateDatabase();
                    // 建立表格
                    CreateTables();
                    // 插入初始資料
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
                // 建立資料庫
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
                // 建立 orders、goods 和 items 表格
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
            // 初始商品資料
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
                    // 下載圖片成為 bytes
                    byte[] imgBytes = GetImageFromUrl(p.Url);
                    // 插入商品資料
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
