namespace HippoOrders
{
    partial class RootMenu
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabsBtn = new System.Windows.Forms.FlowLayoutPanel();
            this.listOrderBtn = new System.Windows.Forms.Button();
            this.addOrderBtn = new System.Windows.Forms.Button();
            this.rootPane = new System.Windows.Forms.Panel();
            this.listGodsBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabsBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("新細明體", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.title.Location = new System.Drawing.Point(12, 17);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(103, 18);
            this.title.TabIndex = 0;
            this.title.Text = "河馬排點菜";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tabsBtn);
            this.panel1.Controls.Add(this.title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 47);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(119, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "喵喵喵";
            // 
            // tabsBtn
            // 
            this.tabsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsBtn.Controls.Add(this.addOrderBtn);
            this.tabsBtn.Controls.Add(this.listOrderBtn);
            this.tabsBtn.Controls.Add(this.listGodsBtn);
            this.tabsBtn.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.tabsBtn.Location = new System.Drawing.Point(551, 3);
            this.tabsBtn.Name = "tabsBtn";
            this.tabsBtn.Size = new System.Drawing.Size(246, 38);
            this.tabsBtn.TabIndex = 1;
            // 
            // listOrderBtn
            // 
            this.listOrderBtn.BackColor = System.Drawing.SystemColors.Control;
            this.listOrderBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.listOrderBtn.FlatAppearance.BorderSize = 0;
            this.listOrderBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listOrderBtn.Location = new System.Drawing.Point(87, 3);
            this.listOrderBtn.Name = "listOrderBtn";
            this.listOrderBtn.Padding = new System.Windows.Forms.Padding(5);
            this.listOrderBtn.Size = new System.Drawing.Size(75, 32);
            this.listOrderBtn.TabIndex = 0;
            this.listOrderBtn.Text = "定單總覽";
            this.listOrderBtn.UseVisualStyleBackColor = false;
            this.listOrderBtn.Click += new System.EventHandler(this.ListOrderBtn_Click);
            // 
            // addOrderBtn
            // 
            this.addOrderBtn.BackColor = System.Drawing.SystemColors.Control;
            this.addOrderBtn.FlatAppearance.BorderSize = 0;
            this.addOrderBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addOrderBtn.Location = new System.Drawing.Point(168, 3);
            this.addOrderBtn.Name = "addOrderBtn";
            this.addOrderBtn.Padding = new System.Windows.Forms.Padding(5);
            this.addOrderBtn.Size = new System.Drawing.Size(75, 32);
            this.addOrderBtn.TabIndex = 1;
            this.addOrderBtn.Text = "購物";
            this.addOrderBtn.UseVisualStyleBackColor = false;
            this.addOrderBtn.Click += new System.EventHandler(this.AddOrderBtn_Click);
            // 
            // rootPane
            // 
            this.rootPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPane.Location = new System.Drawing.Point(0, 47);
            this.rootPane.Name = "rootPane";
            this.rootPane.Size = new System.Drawing.Size(800, 403);
            this.rootPane.TabIndex = 5;
            // 
            // listGodsBtn
            // 
            this.listGodsBtn.BackColor = System.Drawing.SystemColors.Control;
            this.listGodsBtn.FlatAppearance.BorderSize = 0;
            this.listGodsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listGodsBtn.Location = new System.Drawing.Point(6, 3);
            this.listGodsBtn.Name = "listGodsBtn";
            this.listGodsBtn.Padding = new System.Windows.Forms.Padding(5);
            this.listGodsBtn.Size = new System.Drawing.Size(75, 32);
            this.listGodsBtn.TabIndex = 2;
            this.listGodsBtn.Text = "商品總覽";
            this.listGodsBtn.UseVisualStyleBackColor = false;
            this.listGodsBtn.Click += new System.EventHandler(this.ListGodsBtn_Click);
            // 
            // RootMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rootPane);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "RootMenu";
            this.Text = "RootMenu";
            this.Load += new System.EventHandler(this.RootMenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabsBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel rootPane;
        private System.Windows.Forms.Button listOrderBtn;
        private System.Windows.Forms.Button addOrderBtn;
        private System.Windows.Forms.FlowLayoutPanel tabsBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button listGodsBtn;
    }
}

