using ClosedXML.Excel;
using System.Data;

namespace Pharmacy_Manager
{
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
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

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView.Dock = DockStyle.Fill;
            dataGridView.Anchor = 
                AnchorStyles.Top | AnchorStyles.Bottom | 
                AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.DefaultCellStyle.Font = 
                new Font("Arial", 12, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = 
                new Font("Arial", 12, FontStyle.Bold);
        }

        private void Medicines_Load(object sender, EventArgs e)
        {
            // Đường dẫn tới file Excel của bạn
            string filePath = @"C:\Users\TanHoang\Downloads\MedicineData.xlsx";

            // Gọi hàm để tải dữ liệu vào DataGridView
            LoadExcelDataToDataGridView(filePath, MedicinesDGV);
        }
    }
}
