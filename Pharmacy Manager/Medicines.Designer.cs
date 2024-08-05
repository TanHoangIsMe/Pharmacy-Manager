namespace Pharmacy_Manager
{
    partial class Medicines
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
            MedicinesDGV = new DataGridView();
            AddBT = new Button();
            DeleteBT = new Button();
            UpdateBT = new Button();
            STTTB = new TextBox();
            NameTB = new TextBox();
            ((System.ComponentModel.ISupportInitialize)MedicinesDGV).BeginInit();
            SuspendLayout();
            // 
            // MedicinesDGV
            // 
            MedicinesDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MedicinesDGV.Location = new Point(12, 80);
            MedicinesDGV.Name = "MedicinesDGV";
            MedicinesDGV.RowHeadersWidth = 51;
            MedicinesDGV.Size = new Size(883, 358);
            MedicinesDGV.TabIndex = 0;
            // 
            // AddBT
            // 
            AddBT.BackColor = Color.FromArgb(255, 192, 255);
            AddBT.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBT.Location = new Point(12, 12);
            AddBT.Name = "AddBT";
            AddBT.Size = new Size(120, 60);
            AddBT.TabIndex = 1;
            AddBT.Text = "Thêm";
            AddBT.UseVisualStyleBackColor = false;
            AddBT.Click += AddBT_Click;
            // 
            // DeleteBT
            // 
            DeleteBT.BackColor = Color.FromArgb(192, 192, 255);
            DeleteBT.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteBT.Location = new Point(148, 12);
            DeleteBT.Name = "DeleteBT";
            DeleteBT.Size = new Size(120, 60);
            DeleteBT.TabIndex = 2;
            DeleteBT.Text = "Xóa";
            DeleteBT.UseVisualStyleBackColor = false;
            DeleteBT.Click += DeleteBT_Click;
            // 
            // UpdateBT
            // 
            UpdateBT.BackColor = Color.FromArgb(192, 255, 255);
            UpdateBT.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UpdateBT.Location = new Point(286, 12);
            UpdateBT.Name = "UpdateBT";
            UpdateBT.Size = new Size(120, 60);
            UpdateBT.TabIndex = 3;
            UpdateBT.Text = "Sửa";
            UpdateBT.UseVisualStyleBackColor = false;
            UpdateBT.Click += UpdateBT_Click;
            // 
            // STTTB
            // 
            STTTB.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            STTTB.Location = new Point(432, 24);
            STTTB.Name = "STTTB";
            STTTB.PlaceholderText = "STT";
            STTTB.Size = new Size(92, 34);
            STTTB.TabIndex = 4;
            // 
            // NameTB
            // 
            NameTB.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NameTB.Location = new Point(549, 24);
            NameTB.Name = "NameTB";
            NameTB.PlaceholderText = "Tên Thuốc";
            NameTB.Size = new Size(346, 34);
            NameTB.TabIndex = 5;
            // 
            // Medicines
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 450);
            Controls.Add(NameTB);
            Controls.Add(STTTB);
            Controls.Add(UpdateBT);
            Controls.Add(DeleteBT);
            Controls.Add(AddBT);
            Controls.Add(MedicinesDGV);
            Name = "Medicines";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Medicines";
            Load += Medicines_Load;
            ((System.ComponentModel.ISupportInitialize)MedicinesDGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView MedicinesDGV;
        private Button AddBT;
        private Button DeleteBT;
        private Button UpdateBT;
        private TextBox STTTB;
        private TextBox NameTB;
    }
}