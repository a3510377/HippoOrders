using System;
using System.Windows.Forms;

namespace HippoOrders.Component
{
    public partial class BasicProductShowcaseComponent : ProductShowcaseBase
    {
        public BasicProductShowcaseComponent()
        {
            InitializeComponent();

            // 將子控制項的點擊事件綁定到父控制項的點擊事件
            foreach (Control c in this.Controls)
            {
                c.Click += Child_Click;
            }
        }

        private void Child_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        protected override Label NameLabelControl => nameLabel;
        protected override Label PriceLabelControl => priceLabel;
        protected override PictureBox ImagePictureBoxControl => pictureBox1;
    }
}
