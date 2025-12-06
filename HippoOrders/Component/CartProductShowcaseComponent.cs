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
            // 初始化顯示的總價
            UpdateTotalPrice();
            // 自定義 NumericUpDown 控制項外觀
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
            // 更新總價顯示
            UpdateTotalPrice();
            // 觸發數量變更事件
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

        // 自定義 NumericUpDown 控制項外觀
        private void CustomNumericUpDown(NumericUpDown nud)
        {
            // 避免重複包裝
            if (nud.Parent is Panel && nud.Parent.Name == "wrapperPanel") return;

            // 調整內部文本框大小以適應自定義按鈕
            int widthOfSpinButtons = nud.Controls[0].Width;
            nud.Controls[0].Dispose();
            nud.Controls[0].MinimumSize = new Size(nud.Controls[0].Width + widthOfSpinButtons + 2, nud.Controls[0].MinimumSize.Height);

            // 創建包裝面板
            Panel container = new Panel
            {
                Name = "wrapperPanel",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,

                Size = nud.Size,
                Location = nud.Location,
                Anchor = nud.Anchor
            };

            // 調整 NumericUpDown 控制項樣式
            nud.BorderStyle = BorderStyle.None;
            nud.BackColor = Color.White;
            nud.TextAlign = HorizontalAlignment.Center;

            // 重新設置父控件
            Control originalParent = nud.Parent;
            originalParent.Controls.Add(container);

            // 移動 NumericUpDown 控制項到包裝面板中
            nud.Parent = container;
            nud.Location = new Point(0, 0);
            nud.Width = container.Width;

            // 創建自定義減號按鈕
            int centerY = (container.Height - nud.PreferredHeight) / 2 + 2;
            nud.Top = centerY;

            // 創建減號按鈕
            container.Click += (s, args) => nud.Focus();
        }

        // 禁用數量編輯功能
        public void DisableQuantityEditing()
        {
            addBtn.Visible = false;
            subBtn.Visible = false;
            countInp.Enabled = false;
        }
    }

    // 數量變更事件參數
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
