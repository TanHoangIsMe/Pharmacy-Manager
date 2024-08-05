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
            ((System.ComponentModel.ISupportInitialize)MedicinesDGV).BeginInit();
            SuspendLayout();
            // 
            // MedicinesDGV
            // 
            MedicinesDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MedicinesDGV.Location = new Point(12, 80);
            MedicinesDGV.Name = "MedicinesDGV";
            MedicinesDGV.RowHeadersWidth = 51;
            MedicinesDGV.Size = new Size(776, 358);
            MedicinesDGV.TabIndex = 0;
            // 
            // Medicines
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MedicinesDGV);
            Name = "Medicines";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Medicines";
            Load += Medicines_Load;
            ((System.ComponentModel.ISupportInitialize)MedicinesDGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView MedicinesDGV;
    }
}