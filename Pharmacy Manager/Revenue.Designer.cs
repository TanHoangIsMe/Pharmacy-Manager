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
            AdddBT = new Button();
            AllRB = new RadioButton();
            InRB = new RadioButton();
            OutRB = new RadioButton();
            yearTB = new TextBox();
            label1 = new Label();
            RevenueChart = new ScottPlot.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)HistoryDTGV).BeginInit();
            SuspendLayout();
            // 
            // MoneyTB
            // 
            MoneyTB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MoneyTB.Location = new Point(14, 414);
            MoneyTB.Name = "MoneyTB";
            MoneyTB.PlaceholderText = "Nhập Tiền...";
            MoneyTB.Size = new Size(469, 38);
            MoneyTB.TabIndex = 2;
            // 
            // AddBT
            // 
            AddBT.BackColor = Color.FromArgb(255, 192, 128);
            AddBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBT.Location = new Point(508, 414);
            AddBT.Name = "AddBT";
            AddBT.Size = new Size(98, 38);
            AddBT.TabIndex = 1;
            AddBT.Text = "Thu";
            AddBT.UseVisualStyleBackColor = false;
            AddBT.Click += AddBT_Click;
            // 
            // HistoryDTGV
            // 
            HistoryDTGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HistoryDTGV.Location = new Point(14, 458);
            HistoryDTGV.Name = "HistoryDTGV";
            HistoryDTGV.RowHeadersWidth = 51;
            HistoryDTGV.Size = new Size(1074, 161);
            HistoryDTGV.TabIndex = 2;
            // 
            // AdddBT
            // 
            AdddBT.BackColor = Color.FromArgb(128, 128, 255);
            AdddBT.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AdddBT.Location = new Point(633, 414);
            AdddBT.Name = "AdddBT";
            AdddBT.Size = new Size(98, 38);
            AdddBT.TabIndex = 3;
            AdddBT.Text = "Chi";
            AdddBT.UseVisualStyleBackColor = false;
            AdddBT.Click += AdddBT_Click;
            // 
            // AllRB
            // 
            AllRB.AutoSize = true;
            AllRB.Checked = true;
            AllRB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AllRB.Location = new Point(762, 415);
            AllRB.Name = "AllRB";
            AllRB.Size = new Size(115, 37);
            AllRB.TabIndex = 4;
            AllRB.TabStop = true;
            AllRB.Text = "Tất Cả";
            AllRB.UseVisualStyleBackColor = true;
            AllRB.CheckedChanged += AllRB_CheckedChanged;
            // 
            // InRB
            // 
            InRB.AutoSize = true;
            InRB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            InRB.Location = new Point(896, 413);
            InRB.Name = "InRB";
            InRB.Size = new Size(78, 37);
            InRB.TabIndex = 5;
            InRB.Text = "Thu";
            InRB.UseVisualStyleBackColor = true;
            InRB.CheckedChanged += InRB_CheckedChanged;
            // 
            // OutRB
            // 
            OutRB.AutoSize = true;
            OutRB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OutRB.Location = new Point(1013, 413);
            OutRB.Name = "OutRB";
            OutRB.Size = new Size(73, 37);
            OutRB.TabIndex = 6;
            OutRB.Text = "Chi";
            OutRB.UseVisualStyleBackColor = true;
            OutRB.CheckedChanged += OutRB_CheckedChanged;
            // 
            // yearTB
            // 
            yearTB.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            yearTB.Location = new Point(91, 6);
            yearTB.Name = "yearTB";
            yearTB.Size = new Size(235, 38);
            yearTB.TabIndex = 7;
            yearTB.TextChanged += yearTB_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(73, 33);
            label1.TabIndex = 8;
            label1.Text = "Năm:";
            // 
            // RevenueChart
            // 
            RevenueChart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            RevenueChart.Location = new Point(14, 53);
            RevenueChart.Margin = new Padding(5, 4, 5, 4);
            RevenueChart.Name = "RevenueChart";
            RevenueChart.Size = new Size(1074, 354);
            RevenueChart.TabIndex = 9;
            // 
            // Revenue
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 631);
            Controls.Add(RevenueChart);
            Controls.Add(label1);
            Controls.Add(yearTB);
            Controls.Add(OutRB);
            Controls.Add(InRB);
            Controls.Add(AllRB);
            Controls.Add(AdddBT);
            Controls.Add(HistoryDTGV);
            Controls.Add(AddBT);
            Controls.Add(MoneyTB);
            Name = "Revenue";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Revenue";
            Load += Revenue_Load;
            Resize += Revenue_Resize;
            ((System.ComponentModel.ISupportInitialize)HistoryDTGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MoneyTB;
        private Button AddBT;
        private DataGridView HistoryDTGV;
        private Button AdddBT;
        private RadioButton AllRB;
        private RadioButton InRB;
        private RadioButton OutRB;
        private TextBox yearTB;
        private Label label1;
        private ScottPlot.FormsPlot RevenueChart;
    }
}