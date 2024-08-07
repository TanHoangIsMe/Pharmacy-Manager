namespace Pharmacy_Manager
{
    partial class Revenue
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
            MoneyTB = new TextBox();
            AddBT = new Button();
            HistoryDTGV = new DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)HistoryDTGV).BeginInit();
            SuspendLayout();
            // 
            // MoneyTB
            // 
            MoneyTB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MoneyTB.Location = new Point(12, 12);
            MoneyTB.Name = "MoneyTB";
            MoneyTB.PlaceholderText = "Nhập Tiền...";
            MoneyTB.Size = new Size(497, 38);
            MoneyTB.TabIndex = 0;
            // 
            // AddBT
            // 
            AddBT.BackColor = Color.FromArgb(192, 192, 255);
            AddBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBT.Location = new Point(515, 12);
            AddBT.Name = "AddBT";
            AddBT.Size = new Size(98, 38);
            AddBT.TabIndex = 1;
            AddBT.Text = "Thêm";
            AddBT.UseVisualStyleBackColor = false;
            AddBT.Click += AddBT_Click;
            // 
            // HistoryDTGV
            // 
            HistoryDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HistoryDTGV.Location = new Point(12, 67);
            HistoryDTGV.Name = "HistoryDTGV";
            HistoryDTGV.RowHeadersWidth = 51;
            HistoryDTGV.Size = new Size(1074, 447);
            HistoryDTGV.TabIndex = 2;
            // 
            // Revenue
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 549);
            Controls.Add(HistoryDTGV);
            Controls.Add(AddBT);
            Controls.Add(MoneyTB);
            Name = "Revenue";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Revenue";
            Load += Revenue_Load;
            ((System.ComponentModel.ISupportInitialize)HistoryDTGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MoneyTB;
        private Button AddBT;
        private DataGridView HistoryDTGV;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}