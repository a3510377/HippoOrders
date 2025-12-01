using System;
using System.Drawing;
using System.Windows.Forms;

namespace HippoOrders.Component
{
    public partial class CartProductShowcaseComponent : ProductShowcaseBase
    {
        public event EventHandler<QuantityChangedEventArgs> QuantityChanged;
        private int lastQuantity = 1;

        public CartProductShowcaseComponent()
        {
            InitializeComponent();
        }

        protected override Label NameLabelControl => nameLabel;
        protected override Label PriceLabelControl => priceLabel;
        protected override PictureBox ImagePictureBoxControl => pictureBox1;

        private void CartProductShowcaseComponent_Load(object sender, EventArgs e)
        {
            UpdateTotalPrice();
            CustomNumericUpDown(countInp);
        }

        private void SubBtn_Click(object sender, EventArgs e)
        {
            countInp.DownButton();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            countInp.UpButton();
        }

        private void CountInp_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
            QuantityChanged?.Invoke(this, new QuantityChangedEventArgs(this.Quantity, lastQuantity));
            lastQuantity = this.Quantity;
        }

        private void UpdateTotalPrice()
        {
            totalPriceLabel.Text = $"$ {this.ProductPrice * Quantity:N0}";
        }

        public int Quantity
        {
            get => (int)countInp.Value;
            set => countInp.Value = value;
        }

        private void CustomNumericUpDown(NumericUpDown nud)
        {
            if (nud.Parent is Panel && nud.Parent.Name == "wrapperPanel") return;

            int widthOfSpinButtons = nud.Controls[0].Width;
            nud.Controls[0].Dispose();
            nud.Controls[0].MinimumSize = new Size(nud.Controls[0].Width + widthOfSpinButtons + 2, nud.Controls[0].MinimumSize.Height);

            Panel container = new Panel
            {
                Name = "wrapperPanel",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,

                Size = nud.Size,
                Location = nud.Location,
                Anchor = nud.Anchor
            };

            nud.BorderStyle = BorderStyle.None;
            nud.BackColor = Color.White;
            nud.TextAlign = HorizontalAlignment.Center;

            Control originalParent = nud.Parent;
            originalParent.Controls.Add(container);

            nud.Parent = container;
            nud.Location = new Point(0, 0);
            nud.Width = container.Width;

            int centerY = (container.Height - nud.PreferredHeight) / 2 + 2;
            nud.Top = centerY;

            container.Click += (s, args) => nud.Focus();
        }

        public void DisableQuantityEditing()
        {
            addBtn.Visible = false;
            subBtn.Visible = false;
            countInp.Enabled = false;
        }
    }

    public class QuantityChangedEventArgs : EventArgs
    {
        public int Quantity { get; }
        public int OldQuantity { get; }

        public QuantityChangedEventArgs(int quantity, int oldQuantity)
        {
            Quantity = quantity;
            OldQuantity = oldQuantity;
        }
    }
}
