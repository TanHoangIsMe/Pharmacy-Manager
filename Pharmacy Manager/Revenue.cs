using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;
using System.Globalization;


namespace Pharmacy_Manager
{
    public partial class Revenue : Form
    {
        DataPath dataPath = new DataPath();
        string filePath;
        DateTime dateTime;

        public Revenue()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            dateTime = DateTime.Now;
            filePath = dataPath.revenueFilePath;
        }

        private void AddBT_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ TextBox và hệ thống
            string moneyText = MoneyTB.Text.Trim();
            int currentYear = dateTime.Year;
            int currentMonth = dateTime.Month;
            int currentDay = dateTime.Day;

            // Kiểm tra nếu TextBox có chứa số tiền hợp lệ
            decimal moneyAmount;
            if (!decimal.TryParse(moneyText, out moneyAmount))
            {
                MessageBox.Show("Hãy nhập đúng số tiền");
                return;
            }

            if(string.IsNullOrWhiteSpace(AddBT.Text))
            {
                MessageBox.Show("Hãy nhập đúng số tiền");
                return;
            }


            // Kiểm tra xem file Excel đã tồn tại chưa
            FileInfo fileInfo = new FileInfo(filePath);
            bool fileExists = fileInfo.Exists;

            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet;

                // Nếu file chưa tồn tại, tạo một worksheet mới
                if (!fileExists)
                {
                    worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells[1, 1].Value = "Year";
                    worksheet.Cells[1, 2].Value = "Month";
                    worksheet.Cells[1, 3].Value = "Day";
                    worksheet.Cells[1, 4].Value = "Amount";
                }
                else
                {
                    worksheet = package.Workbook.Worksheets[0];
                }

                // Xác định hàng tiếp theo để thêm dữ liệu
                int nextRow = worksheet.Dimension == null ? 2 : worksheet.Dimension.End.Row + 1;

                // Thêm dữ liệu vào worksheet
                worksheet.Cells[nextRow, 1].Value = currentYear;
                worksheet.Cells[nextRow, 2].Value = currentMonth;
                worksheet.Cells[nextRow, 3].Value = currentDay;
                worksheet.Cells[nextRow, 4].Value = moneyAmount;

                // Lưu thay đổi vào file
                package.Save();
            }
            UpdateDTGV();
            MessageBox.Show("Cập Nhật Tiền Thành Công.");
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

        private void Revenue_Load(object sender, EventArgs e)
        {
            UpdateDTGV();
        }

        private void UpdateDTGV()
        {
            // Gọi hàm để tải dữ liệu vào DataGridView
            LoadExcelDataToDataGridView(filePath, HistoryDTGV);
            HistoryDTGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            HistoryDTGV.Columns[0].Width = 150;
            HistoryDTGV.Columns[1].Width = 150;
            HistoryDTGV.Columns[2].Width = 150;
            HistoryDTGV.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            HistoryDTGV.DefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
            HistoryDTGV.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
        }
    }
}
