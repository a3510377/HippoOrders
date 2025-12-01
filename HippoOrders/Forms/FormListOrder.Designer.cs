namespace HippoOrders.Forms
{
    partial class FormListOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ordersBox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ordersBox
            // 
            this.ordersBox.AutoScroll = true;
            this.ordersBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordersBox.Location = new System.Drawing.Point(0, 0);
            this.ordersBox.Name = "ordersBox";
            this.ordersBox.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.ordersBox.Size = new System.Drawing.Size(800, 450);
            this.ordersBox.TabIndex = 0;
            // 
            // FormListOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ordersBox);
            this.Name = "FormListOrder";
            this.Text = "FormListOrder";
            this.Load += new System.EventHandler(this.FormListOrder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ordersBox;
    }
}