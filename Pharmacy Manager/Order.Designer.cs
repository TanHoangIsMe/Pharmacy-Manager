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
            AddBT = new Button();
            DeleteBT = new Button();
            ExportBT = new Button();
            SearchTB = new TextBox();
            SaveBT = new Button();
            OpenBT = new Button();
            SearchOrderTB = new TextBox();
            CreateBT = new Button();
            ((System.ComponentModel.ISupportInitialize)MedicinesDTGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrderDTGV).BeginInit();
            SuspendLayout();
            // 
            // MedicinesDTGV
            // 
            MedicinesDTGV.Anchor = AnchorStyles.None;
            MedicinesDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MedicinesDTGV.Location = new Point(12, 100);
            MedicinesDTGV.Name = "MedicinesDTGV";
            MedicinesDTGV.RowHeadersWidth = 51;
            MedicinesDTGV.Size = new Size(535, 464);
            MedicinesDTGV.TabIndex = 0;
            MedicinesDTGV.CellClick += MedicinesDTGV_CellClick;
            // 
            // OrderDTGV
            // 
            OrderDTGV.Anchor = AnchorStyles.None;
            OrderDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OrderDTGV.Location = new Point(553, 100);
            OrderDTGV.Name = "OrderDTGV";
            OrderDTGV.RowHeadersWidth = 51;
            OrderDTGV.Size = new Size(625, 464);
            OrderDTGV.TabIndex = 1;
            OrderDTGV.CellClick += OrderDTGV_CellClick;
            // 
            // AddBT
            // 
            AddBT.BackColor = Color.FromArgb(255, 192, 192);
            AddBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBT.Location = new Point(120, 56);
            AddBT.Name = "AddBT";
            AddBT.Size = new Size(84, 38);
            AddBT.TabIndex = 2;
            AddBT.Text = "Thêm";
            AddBT.UseVisualStyleBackColor = false;
            AddBT.Click += AddBT_Click;
            // 
            // DeleteBT
            // 
            DeleteBT.BackColor = Color.FromArgb(255, 255, 192);
            DeleteBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteBT.Location = new Point(210, 56);
            DeleteBT.Name = "DeleteBT";
            DeleteBT.Size = new Size(84, 38);
            DeleteBT.TabIndex = 3;
            DeleteBT.Text = "Xóa";
            DeleteBT.UseVisualStyleBackColor = false;
            DeleteBT.Click += DeleteBT_Click;
            // 
            // ExportBT
            // 
            ExportBT.BackColor = Color.FromArgb(192, 255, 255);
            ExportBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExportBT.Location = new Point(525, 56);
            ExportBT.Name = "ExportBT";
            ExportBT.Size = new Size(112, 38);
            ExportBT.TabIndex = 4;
            ExportBT.Text = "Xuất Ảnh";
            ExportBT.UseVisualStyleBackColor = false;
            ExportBT.Click += ExportBT_Click;
            // 
            // SearchTB
            // 
            SearchTB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SearchTB.Location = new Point(12, 12);
            SearchTB.Name = "SearchTB";
            SearchTB.PlaceholderText = "Tìm Thuốc Trong Danh Sách...";
            SearchTB.Size = new Size(535, 38);
            SearchTB.TabIndex = 6;
            SearchTB.TextChanged += SearchTB_TextChanged;
            // 
            // SaveBT
            // 
            SaveBT.BackColor = Color.FromArgb(255, 192, 255);
            SaveBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveBT.Location = new Point(300, 56);
            SaveBT.Name = "SaveBT";
            SaveBT.Size = new Size(107, 38);
            SaveBT.TabIndex = 7;
            SaveBT.Text = "Lưu File";
            SaveBT.UseVisualStyleBackColor = false;
            SaveBT.Click += SaveBT_Click;
            // 
            // OpenBT
            // 
            OpenBT.BackColor = Color.FromArgb(192, 255, 192);
            OpenBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OpenBT.Location = new Point(413, 56);
            OpenBT.Name = "OpenBT";
            OpenBT.Size = new Size(106, 38);
            OpenBT.TabIndex = 8;
            OpenBT.Text = "Mở File";
            OpenBT.UseVisualStyleBackColor = false;
            OpenBT.Click += OpenBT_Click;
            // 
            // SearchOrderTB
            // 
            SearchOrderTB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchOrderTB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SearchOrderTB.Location = new Point(553, 12);
            SearchOrderTB.Name = "SearchOrderTB";
            SearchOrderTB.PlaceholderText = "Tìm Thuốc Đã Đặt...";
            SearchOrderTB.Size = new Size(625, 38);
            SearchOrderTB.TabIndex = 9;
            SearchOrderTB.TextChanged += SearchOrderTB_TextChanged;
            // 
            // CreateBT
            // 
            CreateBT.BackColor = Color.FromArgb(255, 224, 192);
            CreateBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CreateBT.Location = new Point(12, 56);
            CreateBT.Name = "CreateBT";
            CreateBT.Size = new Size(102, 38);
            CreateBT.TabIndex = 10;
            CreateBT.Text = "Tạo Đơn";
            CreateBT.UseVisualStyleBackColor = false;
            CreateBT.Click += CreateBT_Click;
            // 
            // Order
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 192, 255);
            ClientSize = new Size(1184, 576);
            Controls.Add(CreateBT);
            Controls.Add(SearchOrderTB);
            Controls.Add(OpenBT);
            Controls.Add(SaveBT);
            Controls.Add(SearchTB);
            Controls.Add(ExportBT);
            Controls.Add(DeleteBT);
            Controls.Add(AddBT);
            Controls.Add(OrderDTGV);
            Controls.Add(MedicinesDTGV);
            ForeColor = Color.Black;
            Name = "Order";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Order";
            Load += Order_Load;
            Resize += Order_Resize;
            ((System.ComponentModel.ISupportInitialize)MedicinesDTGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrderDTGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView MedicinesDTGV;
        private DataGridView OrderDTGV;
        private Button AddBT;
        private Button DeleteBT;
        private Button ExportBT;
        private TextBox SearchTB;
        private Button SaveBT;
        private Button OpenBT;
        private TextBox SearchOrderTB;
        private Button CreateBT;
    }
}