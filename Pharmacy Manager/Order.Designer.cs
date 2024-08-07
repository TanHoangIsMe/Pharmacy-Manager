namespace Pharmacy_Manager
{
    partial class Order
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
            MedicinesDTGV = new DataGridView();
            OrderDTGV = new DataGridView();
            STT = new DataGridViewTextBoxColumn();
            MedicineName = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            AddBT = new Button();
            DeleteBT = new Button();
            ExportBT = new Button();
            ImageBT = new Button();
            ((System.ComponentModel.ISupportInitialize)MedicinesDTGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrderDTGV).BeginInit();
            SuspendLayout();
            // 
            // MedicinesDTGV
            // 
            MedicinesDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MedicinesDTGV.Location = new Point(12, 12);
            MedicinesDTGV.Name = "MedicinesDTGV";
            MedicinesDTGV.RowHeadersWidth = 51;
            MedicinesDTGV.Size = new Size(540, 552);
            MedicinesDTGV.TabIndex = 0;
            MedicinesDTGV.CellClick += MedicinesDTGV_CellClick;
            // 
            // OrderDTGV
            // 
            OrderDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OrderDTGV.Columns.AddRange(new DataGridViewColumn[] { STT, MedicineName, Quantity });
            OrderDTGV.Location = new Point(670, 12);
            OrderDTGV.Name = "OrderDTGV";
            OrderDTGV.RowHeadersWidth = 51;
            OrderDTGV.Size = new Size(502, 552);
            OrderDTGV.TabIndex = 1;
            OrderDTGV.CellClick += OrderDTGV_CellClick;
            // 
            // STT
            // 
            STT.HeaderText = "STT";
            STT.MinimumWidth = 6;
            STT.Name = "STT";
            STT.Width = 125;
            // 
            // MedicineName
            // 
            MedicineName.HeaderText = "Tên Thuốc";
            MedicineName.MinimumWidth = 6;
            MedicineName.Name = "MedicineName";
            MedicineName.Width = 125;
            // 
            // Quantity
            // 
            Quantity.HeaderText = "Số Lượng";
            Quantity.MinimumWidth = 6;
            Quantity.Name = "Quantity";
            Quantity.Width = 125;
            // 
            // AddBT
            // 
            AddBT.BackColor = Color.FromArgb(255, 192, 192);
            AddBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBT.Location = new Point(569, 112);
            AddBT.Name = "AddBT";
            AddBT.Size = new Size(84, 68);
            AddBT.TabIndex = 2;
            AddBT.Text = "Thêm";
            AddBT.UseVisualStyleBackColor = false;
            AddBT.Click += AddBT_Click;
            // 
            // DeleteBT
            // 
            DeleteBT.BackColor = Color.FromArgb(255, 255, 192);
            DeleteBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteBT.Location = new Point(569, 199);
            DeleteBT.Name = "DeleteBT";
            DeleteBT.Size = new Size(84, 68);
            DeleteBT.TabIndex = 3;
            DeleteBT.Text = "Xóa";
            DeleteBT.UseVisualStyleBackColor = false;
            DeleteBT.Click += DeleteBT_Click;
            // 
            // ExportBT
            // 
            ExportBT.BackColor = Color.FromArgb(192, 255, 255);
            ExportBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExportBT.Location = new Point(569, 290);
            ExportBT.Name = "ExportBT";
            ExportBT.Size = new Size(84, 68);
            ExportBT.TabIndex = 4;
            ExportBT.Text = "Xuất File";
            ExportBT.UseVisualStyleBackColor = false;
            ExportBT.Click += ExportBT_Click;
            // 
            // ImageBT
            // 
            ImageBT.BackColor = Color.FromArgb(192, 255, 192);
            ImageBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ImageBT.Location = new Point(569, 383);
            ImageBT.Name = "ImageBT";
            ImageBT.Size = new Size(84, 68);
            ImageBT.TabIndex = 5;
            ImageBT.Text = "Tạo Ảnh";
            ImageBT.UseVisualStyleBackColor = false;
            ImageBT.Click += ImageBT_Click;
            // 
            // Order
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1184, 576);
            Controls.Add(ImageBT);
            Controls.Add(ExportBT);
            Controls.Add(DeleteBT);
            Controls.Add(AddBT);
            Controls.Add(OrderDTGV);
            Controls.Add(MedicinesDTGV);
            Name = "Order";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Order";
            Load += Order_Load;
            Resize += Order_Resize;
            ((System.ComponentModel.ISupportInitialize)MedicinesDTGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrderDTGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView MedicinesDTGV;
        private DataGridView OrderDTGV;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn MedicineName;
        private DataGridViewTextBoxColumn Quantity;
        private Button AddBT;
        private Button DeleteBT;
        private Button ExportBT;
        private Button ImageBT;
    }
}