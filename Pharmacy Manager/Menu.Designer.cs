namespace Pharmacy_Manager
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            MedicinesBT = new Button();
            OrderBT = new Button();
            RevenueBT = new Button();
            SuspendLayout();
            // 
            // MedicinesBT
            // 
            MedicinesBT.BackColor = Color.FromArgb(255, 192, 192);
            MedicinesBT.Font = new Font("Arial Narrow", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MedicinesBT.Location = new Point(12, 128);
            MedicinesBT.Name = "MedicinesBT";
            MedicinesBT.Size = new Size(250, 200);
            MedicinesBT.TabIndex = 0;
            MedicinesBT.Text = "Cập Nhật Thuốc";
            MedicinesBT.UseVisualStyleBackColor = false;
            MedicinesBT.Click += MedicinesBT_Click;
            // 
            // OrderBT
            // 
            OrderBT.BackColor = Color.FromArgb(255, 255, 192);
            OrderBT.Font = new Font("Arial Narrow", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OrderBT.Location = new Point(341, 128);
            OrderBT.Name = "OrderBT";
            OrderBT.Size = new Size(250, 200);
            OrderBT.TabIndex = 1;
            OrderBT.Text = "Đặt Hàng";
            OrderBT.UseVisualStyleBackColor = false;
            OrderBT.Click += OrderBT_Click;
            // 
            // RevenueBT
            // 
            RevenueBT.BackColor = Color.FromArgb(192, 255, 192);
            RevenueBT.Font = new Font("Arial Narrow", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RevenueBT.Location = new Point(658, 128);
            RevenueBT.Name = "RevenueBT";
            RevenueBT.Size = new Size(250, 200);
            RevenueBT.TabIndex = 2;
            RevenueBT.Text = "Doanh Thu";
            RevenueBT.UseVisualStyleBackColor = false;
            RevenueBT.Click += RevenueBT_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(920, 468);
            Controls.Add(RevenueBT);
            Controls.Add(OrderBT);
            Controls.Add(MedicinesBT);
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            ResumeLayout(false);
        }

        #endregion

        private Button MedicinesBT;
        private Button OrderBT;
        private Button RevenueBT;
    }
}