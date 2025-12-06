using HippoOrders.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HippoOrders
{
    public partial class RootMenu : Form
    {
        // 各個子表單的實例
        private readonly FormListOrder formListOrder = new FormListOrder();
        private readonly FormListGods formListGods = new FormListGods();
        private readonly FormAddOrder formAddOrder = new FormAddOrder();

        // 當前活動的表單
        private Form activeForm = null;

        public RootMenu()
        {
            InitializeComponent();
        }

        // 將指定的表單打開在 rootPane 中
        private void OpenFormInRootPane(Form form)
        {
            // 如果未改變，不做任何操作
            if (activeForm == form) return;

            // 隱藏當前活動的表單
            activeForm?.Hide();
            // 設置新的活動表單
            activeForm = form;

            // 將表單添加到 rootPane 中（如果尚未添加）
            if (!rootPane.Controls.Contains(form))
            {
                // 設置為非頂級表單
                form.TopLevel = false;
                // 去除邊框
                form.FormBorderStyle = FormBorderStyle.None;
                // 停靠填充
                form.Dock = DockStyle.Fill;

                // 添加到 rootPane
                rootPane.Controls.Add(form);
            }

            // 顯示表單
            form.Show();
            // 更新按鈕的活動狀態
            UpdateBtnActiveState();
        }

        private void RootMenu_Load(object sender, EventArgs e)
        {
            OpenFormInRootPane(formAddOrder);
        }

        private void ListGodsBtn_Click(object sender, EventArgs e)
        {
            OpenFormInRootPane(formListGods);
        }

        private void ListOrderBtn_Click(object sender, EventArgs e)
        {
            OpenFormInRootPane(formListOrder);
        }

        private void AddOrderBtn_Click(object sender, EventArgs e)
        {
            OpenFormInRootPane(formAddOrder);
        }

        private void UpdateBtnActiveState()
        {
            // 重置所有按鈕的背景顏色
            foreach (Control control in tabsBtn.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }

            // 根據當前活動的表單設置相應按鈕的背景顏色
            Button activeButton = null;
            if (activeForm is FormListGods) activeButton = listGodsBtn;
            else if (activeForm is FormAddOrder) activeButton = addOrderBtn;
            else if (activeForm is FormListOrder listOrderForm)
            {
                activeButton = listOrderBtn;
                // 在切換到訂單列表時重新加載訂單
                listOrderForm.LoadOrdersFromDb();
            }

            if (activeButton != null)
            {
                activeButton.BackColor = SystemColors.ActiveCaption;
            }
        }
    }
}
