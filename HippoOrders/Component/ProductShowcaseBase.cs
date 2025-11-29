using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace HippoOrders.Component
{
    public class ProductShowcaseBase : UserControl
    {
        protected virtual Label NameLabelControl => null;
        protected virtual Label PriceLabelControl => null;
        protected virtual PictureBox ImagePictureBoxControl => null;

        public int ProductID { get; set; } = 0;

        public new string ProductName
        {
            get => NameLabelControl?.Text ?? "";
            set
            {
                if (NameLabelControl != null) NameLabelControl.Text = value;
            }
        }

        public decimal ProductPrice
        {
            get
            {
                if (PriceLabelControl == null) return 0;
                decimal.TryParse(PriceLabelControl.Text.Replace("$", "").Trim(), out decimal v);
                return v;
            }
            set
            {
                if (PriceLabelControl != null) PriceLabelControl.Text = $"$ {value:N0}";
            }
        }

        public Image ProductImage
        {
            get => ImagePictureBoxControl?.Image;
            set
            {
                if (ImagePictureBoxControl != null) ImagePictureBoxControl.Image = value;
            }
        }

        public void SetImageFromBytes(byte[] bytes)
        {
            if (ImagePictureBoxControl == null) return;

            if (bytes == null || bytes.Length == 0)
            {
                ImagePictureBoxControl.Image = null;
                return;
            }

            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    ImagePictureBoxControl.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                ImagePictureBoxControl.Image = null;
            }
        }
    }
}
