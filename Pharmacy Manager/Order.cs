using ClosedXML.Excel;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Manager
{
    public partial class Order : Form
    {
        DataPath dataPath = new DataPath();
        string filePath;
        private int selectedRowIndex = -1;

        public Order()
        {
            InitializeComponent();
            MedicinesDTGV.Dock = DockStyle.Left;
            OrderDTGV.Dock = DockStyle.Right;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            filePath = dataPath.filePath;
            // Gọi hàm để tải dữ liệu vào DataGridView
            LoadExcelDataToDataGridView(filePath, MedicinesDTGV);
            ResizeDTGV();
            SettingDTGV(MedicinesDTGV);
            SettingDTGV(OrderDTGV);
            MedicinesDTGV.ReadOnly = true;
            OrderDTGV.AllowUserToAddRows = false;
        }

        public void LoadExcelDataToDataGridView(string filePath, DataGridView dataGridView)
        {
            // Tạo một DataTable để lưu dữ liệu
            DataTable dataTable = new DataTable();

            // Mở file Excel
            using (var workbook = new XLWorkbook(filePath))
            {
                // Chọn sheet đầu tiên
                var worksheet = workbook.Worksheet(1);

                // Đọc tiêu đề cột từ hàng đầu tiên
                var firstRow = worksheet.Row(1);
                foreach (var cell in firstRow.Cells())
                {
                    dataTable.Columns.Add(cell.Value.ToString());
                }

                // Đọc dữ liệu từ các hàng tiếp theo
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Cells().Count(); i++)
                    {
                        dataRow[i] = row.Cell(i + 1).Value.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            // Gán DataTable cho DataGridView
            dataGridView.DataSource = dataTable;
        }

        void SettingDTGV(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.DefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
        }

        private void AddBT_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn
            if (MedicinesDTGV.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = MedicinesDTGV.SelectedRows[0];

                // Lấy giá trị của cột thứ hai từ hàng được chọn
                string columnValue = selectedRow.Cells[1].Value.ToString();

                // Tạo số thứ tự cho hàng mới (thực tế bạn có thể muốn tự động tăng số thứ tự này)
                int rowCount = OrderDTGV.Rows.Count;

                // Thêm hàng mới vào d2
                OrderDTGV.Rows.Add(rowCount, columnValue, 0);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thuốc!");
            }
        }

        private void Order_Resize(object sender, EventArgs e)
        {
            ResizeDTGV();
        }

        private void ResizeDTGV()
        {
            // Cập nhật kích thước của d1 và d2 khi Form thay đổi kích thước
            MedicinesDTGV.Width = this.ClientSize.Width / 2 - 50;
            OrderDTGV.Width = this.ClientSize.Width / 2 - 50;
            OrderDTGV.Left = MedicinesDTGV.Right;

            // Tính toán vị trí để Button nằm giữa màn hình
            int centerX = (this.ClientSize.Width - AddBT.Width) / 2;
            int centerY = (this.ClientSize.Height - AddBT.Height) / 2;

            // Đặt vị trí của Button
            AddBT.Location = new Point(centerX, centerY);
        }

        private void MedicinesDTGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu nhấn vào một ô hợp lệ
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Chọn toàn bộ hàng
                MedicinesDTGV.Rows[e.RowIndex].Selected = true;
            }
        }

        private void OrderDTGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lưu chỉ số hàng được chọn khi nhấn vào ô
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                OrderDTGV.Rows[e.RowIndex].Selected = true;
                selectedRowIndex = e.RowIndex;
            }
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            // Kiểm tra chỉ số hàng có hợp lệ không
            if (selectedRowIndex >= 0)
            {
                // Xóa hàng đã chọn
                OrderDTGV.Rows.RemoveAt(selectedRowIndex);

                // Cập nhật lại số thứ tự cho tất cả các hàng còn lại
                UpdateRowNumbers();

                // Đặt lại chỉ số hàng đã chọn
                selectedRowIndex = -1;
            }
        }

        private void UpdateRowNumbers()
        {
            // Cập nhật số thứ tự cho tất cả các hàng
            for (int i = 0; i < OrderDTGV.Rows.Count; i++)
            {
                OrderDTGV.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }
    }
}
