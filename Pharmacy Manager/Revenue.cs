using ClosedXML.Excel;
using OfficeOpenXml;
using ScottPlot;
using System.Data;
using System.Windows.Forms;

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
            AddDataToExcel(true);
        }

        private void AdddBT_Click(object sender, EventArgs e)
        {
            AddDataToExcel(false);
        }

        public void AddDataToExcel(bool type)
        {
            // Lấy thông tin từ TextBox và hệ thống
            string moneyText = MoneyTB.Text.Trim();
            int currentYear = dateTime.Year;
            int currentMonth = dateTime.Month;
            int currentDay = dateTime.Day;

            // Kiểm tra nếu TextBox có chứa số tiền hợp lệ
            if (!decimal.TryParse(moneyText, out decimal moneyAmount))
            {
                MessageBox.Show("Hãy nhập đúng số tiền");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddBT.Text))
            {
                MessageBox.Show("Hãy nhập đúng số tiền");
                return;
            }

            try
            {
                // Kiểm tra xem file Excel đã tồn tại chưa
                FileInfo fileInfo = new FileInfo(filePath);
                bool fileExists = fileInfo.Exists;

                using (var package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;

                    // Nếu file chưa tồn tại, tạo một worksheet mới
                    if (!fileExists)
                    {
                        worksheet = package.Workbook.Worksheets.Add("RevenueData");
                        worksheet.Cells[1, 1].Value = "Loại";
                        worksheet.Cells[1, 2].Value = "Năm";
                        worksheet.Cells[1, 3].Value = "Tháng";
                        worksheet.Cells[1, 4].Value = "Ngày";
                        worksheet.Cells[1, 5].Value = "Số Tiền";
                    }
                    else
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }

                    // Xác định hàng tiếp theo để thêm dữ liệu
                    int nextRow = worksheet.Dimension == null ? 2 : worksheet.Dimension.End.Row + 1;

                    // Thêm dữ liệu vào worksheet
                    worksheet.Cells[nextRow, 1].Value = type ? "THU" : "CHI";
                    worksheet.Cells[nextRow, 2].Value = currentYear;
                    worksheet.Cells[nextRow, 3].Value = currentMonth;
                    worksheet.Cells[nextRow, 4].Value = currentDay;
                    worksheet.Cells[nextRow, 5].Value = moneyAmount;

                    // Lưu thay đổi vào file
                    package.Save();
                }
                UpdateDTGV();
                MessageBox.Show("Cập Nhật Tiền Thành Công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
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
            // Đặt thuộc tính Anchor cho ScottPlot
            RevenueChart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HistoryDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            UpdateDTGV();
            // Cập nhật TextBox với năm hiện tại
            yearTB.Text = DateTime.Now.Year.ToString();
        }

        private void UpdateDTGV()
        {
            // Gọi hàm để tải dữ liệu vào DataGridView
            LoadExcelDataToDataGridView(filePath, HistoryDTGV);
            HistoryDTGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            HistoryDTGV.Columns[0].Width = 100;
            HistoryDTGV.Columns[1].Width = 100;
            HistoryDTGV.Columns[2].Width = 100;
            HistoryDTGV.Columns[3].Width = 100;
            HistoryDTGV.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            HistoryDTGV.DefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
            HistoryDTGV.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
        }

        private void InRB_CheckedChanged(object sender, EventArgs e)
        {
            string searchText = "THU";
            // Kiểm tra xem DataGridView có DataSource là DataTable không
            if (HistoryDTGV.DataSource is DataTable dataTable)
            {
                // Tạo bộ lọc cho DataTable
                // Lấy tên cột thứ hai từ DataTable
                string columnName = dataTable.Columns[0].ColumnName;

                // Tạo biểu thức lọc cho DataTable
                string filterExpression = $"[{columnName}] LIKE '%{searchText}%'";

                // Áp dụng bộ lọc cho DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
            }
        }

        private void OutRB_CheckedChanged(object sender, EventArgs e)
        {
            string searchText = "CHI";
            // Kiểm tra xem DataGridView có DataSource là DataTable không
            if (HistoryDTGV.DataSource is DataTable dataTable)
            {
                // Tạo bộ lọc cho DataTable
                // Lấy tên cột thứ hai từ DataTable
                string columnName = dataTable.Columns[0].ColumnName;

                // Tạo biểu thức lọc cho DataTable
                string filterExpression = $"[{columnName}] LIKE '%{searchText}%'";

                // Áp dụng bộ lọc cho DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
            }
        }

        private void AllRB_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem DataGridView có DataSource là DataTable không
            if (HistoryDTGV.DataSource is DataTable dataTable)
            {
                // Xóa bộ lọc để hiển thị tất cả dữ liệu
                dataTable.DefaultView.RowFilter = string.Empty;
            }
        }

        private void LoadData()
        {
            DataTable dataTable = ReadExcelFile(filePath);

            // Nhóm dữ liệu theo năm và tháng
            var groupedData = dataTable.AsEnumerable()
                .GroupBy(row => new
                {
                    Year = Convert.ToInt32(row.Field<string>("Năm")),
                    Month = Convert.ToInt32(row.Field<string>("Tháng"))
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    InTotal = g.Where(row => row.Field<string>("Loại") == "THU").Sum(row => decimal.Parse(row.Field<string>("Số Tiền"))),
                    OutTotal = g.Where(row => row.Field<string>("Loại") == "CHI").Sum(row => decimal.Parse(row.Field<string>("Số Tiền")))
                })
                .Where(d => d.Year.ToString() == yearTB.Text);

            // Chuẩn bị dữ liệu cho biểu đồ
            var months = Enumerable.Range(1, 12).Select(m => (double)m).ToArray();
            var inValues = months.Select(m => groupedData.FirstOrDefault(d => d.Month == m)?.InTotal ?? 0).Select(d => (double)d).ToArray();
            var outValues = months.Select(m => groupedData.FirstOrDefault(d => d.Month == m)?.OutTotal ?? 0).Select(d => (double)d).ToArray();

            // Vẽ biểu đồ
            var plt = RevenueChart.Plot;
            // Độ rộng của các cột
            double barWidth = 0.4;

            // Khoảng cách giữa các cột
            double offset = barWidth / 2;

            plt.Clear();

            // Tính toán vị trí của các cột
            double[] inPositions = months.Select(m => m - offset).ToArray();
            double[] outPositions = months.Select(m => m + offset).ToArray();

            // Thêm cột In
            var inBar = plt.AddBar(inValues, inPositions, color: System.Drawing.Color.Orange);
            inBar.BarWidth = barWidth;
            inBar.Label = "Thu";
            // Thêm cột Out
            var outBar = plt.AddBar(outValues, outPositions, color: System.Drawing.Color.MediumPurple);
            outBar.BarWidth = barWidth;
            outBar.Label = "Chi";
            // Đặt các thuộc tính cho biểu đồ
            // Cài đặt nhãn trục X
            plt.XAxis.Label("Tháng", size: 22, fontName: "Arial");

            // Cài đặt nhãn trục Y
            plt.YAxis.Label("Số Tiền", size: 22, fontName: "Arial");

            // Cài đặt kích thước và kiểu chữ cho nhãn trục X
            plt.XAxis.TickLabelStyle(fontName: "Arial", fontSize: 18);

            // Cài đặt kích thước và kiểu chữ cho nhãn trục Y
            plt.YAxis.TickLabelStyle(fontName: "Arial", fontSize: 18);
            plt.XAxis.MinimumTickSpacing(1);
            plt.Legend();
            plt.SetAxisLimitsX(0.5, 12.5);
            RevenueChart.Configuration.Zoom = false;
            RevenueChart.Configuration.Pan = false;
            RevenueChart.Refresh();
        }

        private DataTable ReadExcelFile(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    dataTable.Columns.Add(worksheet.Cells[1, col].Text);
                }

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var dataRow = dataTable.NewRow();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        private void yearTB_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Revenue_Resize(object sender, EventArgs e)
        {
            // Chỉ thay đổi chiều rộng, giữ nguyên chiều cao
            RevenueChart.Width = this.ClientSize.Width - RevenueChart.Left * 2;
            // Chiều cao của DataGridView được cập nhật để phù hợp với kích thước form
            HistoryDTGV.Height = this.ClientSize.Height - HistoryDTGV.Top;
        }
    }
}
