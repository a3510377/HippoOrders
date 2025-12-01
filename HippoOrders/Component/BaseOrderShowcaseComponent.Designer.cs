namespace HippoOrders.Component
{
    partial class BaseOrderShowcaseComponent
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.showListBtn = new System.Windows.Forms.PictureBox();
            this.infoTextLab = new System.Windows.Forms.Label();
            this.userNameLab = new System.Windows.Forms.Label();
            this.orderGodListBox = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showListBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.showListBtn);
            this.panel1.Controls.Add(this.infoTextLab);
            this.panel1.Controls.Add(this.userNameLab);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 41);
            this.panel1.TabIndex = 0;
            // 
            // showListBtn
            // 
            this.showListBtn.Image = global::HippoOrders.Properties.Resources.arrow_drop_down_24dp_434343_FILL0_wght400_GRAD0_opsz24;
            this.showListBtn.Location = new System.Drawing.Point(10, 9);
            this.showListBtn.Name = "showListBtn";
            this.showListBtn.Size = new System.Drawing.Size(24, 24);
            this.showListBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.showListBtn.TabIndex = 2;
            this.showListBtn.TabStop = false;
            this.showListBtn.Click += new System.EventHandler(this.ShowListBtn_Click);
            // 
            // infoTextLab
            // 
            this.infoTextLab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTextLab.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoTextLab.Location = new System.Drawing.Point(386, 19);
            this.infoTextLab.Name = "infoTextLab";
            this.infoTextLab.Size = new System.Drawing.Size(242, 15);
            this.infoTextLab.TabIndex = 1;
            this.infoTextLab.Text = "InfoText";
            this.infoTextLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // userNameLab
            // 
            this.userNameLab.AutoSize = true;
            this.userNameLab.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.userNameLab.Location = new System.Drawing.Point(35, 14);
            this.userNameLab.Name = "userNameLab";
            this.userNameLab.Size = new System.Drawing.Size(66, 15);
            this.userNameLab.TabIndex = 0;
            this.userNameLab.Text = "UserName";
            // 
            // orderGodListBox
            // 
            this.orderGodListBox.AutoSize = true;
            this.orderGodListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderGodListBox.Location = new System.Drawing.Point(0, 41);
            this.orderGodListBox.Name = "orderGodListBox";
            this.orderGodListBox.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.orderGodListBox.Size = new System.Drawing.Size(636, 40);
            this.orderGodListBox.TabIndex = 1;
            // 
            // BaseOrderShowcaseComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.orderGodListBox);
            this.Controls.Add(this.panel1);
            this.Name = "BaseOrderShowcaseComponent";
            this.Size = new System.Drawing.Size(636, 81);
            this.Load += new System.EventHandler(this.BaseOrderShowcaseComponent_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showListBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel orderGodListBox;
        private System.Windows.Forms.Label userNameLab;
        private System.Windows.Forms.Label infoTextLab;
        private System.Windows.Forms.PictureBox showListBtn;
    }
}
