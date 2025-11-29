using HippoOrders.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HippoOrders
{
    public partial class RootMenu : Form
    {
        private readonly FormListOrder formListOrder = new FormListOrder();
        private readonly FormListGods formListGods = new FormListGods();
        private readonly FormAddOrder formAddOrder = new FormAddOrder();

        private Form activeForm = null;

        public RootMenu()
        {
            InitializeComponent();
        }

        private void OpenFormInRootPane(Form form)
        {
            if (activeForm == form) return;

            activeForm?.Hide();
            activeForm = form;

            if (!rootPane.Controls.Contains(form))
            {
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                rootPane.Controls.Add(form);
            }

            form.Show();
            UpdateBtnActiveState();
        }

        private void RootMenu_Load(object sender, EventArgs e)
        {
            OpenFormInRootPane(formListOrder);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
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
            foreach (Control control in tabsBtn.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }

            Button activeButton = null;
            if (activeForm is FormListGods) activeButton  = listGodsBtn;
            else if (activeForm is FormAddOrder) activeButton  = addOrderBtn;
            else if (activeForm is FormListOrder) activeButton  = listOrderBtn;

            if (activeButton != null)
            {
                activeButton.BackColor = SystemColors.ActiveCaption;
            }
        }
    }
}
