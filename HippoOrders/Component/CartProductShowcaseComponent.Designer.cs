namespace HippoOrders.Component
{
    partial class CartProductShowcaseComponent
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.priceLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.subBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addBtn = new System.Windows.Forms.Button();
            this.countInp = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countInp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // priceLabel
            // 
            this.priceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.priceLabel.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.priceLabel.ForeColor = System.Drawing.Color.Red;
            this.priceLabel.Location = new System.Drawing.Point(170, 35);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(80, 14);
            this.priceLabel.TabIndex = 5;
            this.priceLabel.Text = "$ 0";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nameLabel.Location = new System.Drawing.Point(90, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(79, 22);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "喵喵喵";
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPriceLabel.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.totalPriceLabel.ForeColor = System.Drawing.Color.Red;
            this.totalPriceLabel.Location = new System.Drawing.Point(92, 5);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(68, 13);
            this.totalPriceLabel.TabIndex = 7;
            this.totalPriceLabel.Text = "$ 0";
            this.totalPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // subBtn
            // 
            this.subBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.subBtn.Location = new System.Drawing.Point(0, 2);
            this.subBtn.Name = "subBtn";
            this.subBtn.Size = new System.Drawing.Size(20, 20);
            this.subBtn.TabIndex = 9;
            this.subBtn.Text = "-";
            this.subBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.subBtn.UseVisualStyleBackColor = true;
            this.subBtn.Click += new System.EventHandler(this.SubBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.addBtn);
            this.panel1.Controls.Add(this.totalPriceLabel);
            this.panel1.Controls.Add(this.subBtn);
            this.panel1.Controls.Add(this.countInp);
            this.panel1.Location = new System.Drawing.Point(90, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 23);
            this.panel1.TabIndex = 9;
            // 
            // addBtn
            // 
            this.addBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addBtn.Location = new System.Drawing.Point(65, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(20, 20);
            this.addBtn.TabIndex = 10;
            this.addBtn.Text = "+";
            this.addBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // countInp
            // 
            this.countInp.Location = new System.Drawing.Point(24, 1);
            this.countInp.Name = "countInp";
            this.countInp.Size = new System.Drawing.Size(38, 22);
            this.countInp.TabIndex = 10;
            this.countInp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.countInp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countInp.ValueChanged += new System.EventHandler(this.CountInp_ValueChanged);
            // 
            // CartProductShowcaseComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "CartProductShowcaseComponent";
            this.Size = new System.Drawing.Size(261, 81);
            this.Load += new System.EventHandler(this.CartProductShowcaseComponent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.countInp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label totalPriceLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button subBtn;
        private System.Windows.Forms.NumericUpDown countInp;
    }
}
