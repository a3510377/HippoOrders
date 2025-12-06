using System;
using System.Windows.Forms;

namespace HippoOrders
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 初始化資料庫
            DbInitializer.Initialize();

            Application.Run(new RootMenu());
        }
    }
}
