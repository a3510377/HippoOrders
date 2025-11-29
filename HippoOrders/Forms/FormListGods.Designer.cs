namespace HippoOrders.Forms
{
    partial class FormListGods
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
            this.godsItemsBox = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // godsItemsBox
            // 
            this.godsItemsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.godsItemsBox.Location = new System.Drawing.Point(0, 0);
            this.godsItemsBox.Name = "godsItemsBox";
            this.godsItemsBox.Size = new System.Drawing.Size(800, 450);
            this.godsItemsBox.TabIndex = 0;
            // 
            // FormListGods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.godsItemsBox);
            this.Name = "FormListGods";
            this.Text = "FormListGods";
            this.Load += new System.EventHandler(this.FormListGods_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel godsItemsBox;
    }
}