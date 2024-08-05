using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;

namespace Pharmacy_Manager
{
    public partial class Medicines : Form
    {
        // Đường dẫn tới file Excel của bạn
        string filePath = @"C:\Users\TanHoang\Downloads\MedicineData.xlsx";

        public Medicines()
        {
            InitializeComponent();
            // Thiết lập ngữ cảnh giấy phép cho EPPlus
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        private void Medicines_Load(object sender, EventArgs e)
        {
            // Gọi hàm để tải dữ liệu vào DataGridView
            LoadExcelDataToDataGridView(filePath, MedicinesDGV);
            MedicinesDGV.ReadOnly = true;
        }

        private void AddBT_Click(object sender, EventArgs e)
        {
            // Đọc dữ liệu từ TextBox
            string newData = NameTB.Text.Trim();
            if (string.IsNullOrEmpty(newData))
            {
                MessageBox.Show("Hãy nhập tên thuốc");
                return;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;

                    if (package.Workbook.Worksheets.Count > 0)
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Thấy File Excel.");
                        return;
                    }

                    int rowCount = worksheet.Dimension?.End.Row ?? 0;
                    int nextNumber = rowCount > 1 ? Convert.ToInt32
                        (worksheet.Cells[rowCount, 1].Value) + 1 : 1;
                    worksheet.Cells[rowCount + 1, 1].Value = nextNumber;
                    worksheet.Cells[rowCount + 1, 2].Value = newData;

                    package.Save();
                }

                UpdateDataGridView();
                MessageBox.Show("Thêm Thuốc Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox để xác định hàng cần xóa
            string sttToDelete = STTTB.Text.Trim();
            if (string.IsNullOrEmpty(sttToDelete))
            {
                MessageBox.Show("Chưa Điền STT Hàng Cần Xóa");
                return;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;
                    if (package.Workbook.Worksheets.Count > 0)
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Thấy File Excel.");
                        return;
                    }

                    // Tìm dòng có giá trị STT cần xóa
                    bool rowFound = false;
                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (worksheet.Cells[row, 1].Text.Trim() == sttToDelete)
                        {
                            // Xóa hàng
                            worksheet.DeleteRow(row);
                            rowFound = true;
                            break;
                        }
                    }

                    if (!rowFound)
                    {
                        MessageBox.Show("STT Không Tồn Tại");
                        return;
                    }

                    // Lưu file Excel
                    package.Save();
                }

                // Cập nhật DataGridView
                UpdateDataGridView();
                MessageBox.Show("Xóa Thuốc Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void UpdateBT_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string sttToUpdate = STTTB.Text.Trim();
            string newName = NameTB.Text.Trim();

            if (string.IsNullOrEmpty(sttToUpdate) || string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Hãy Điền Đủ Thông Tin");
                return;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;
                    if (package.Workbook.Worksheets.Count > 0)
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Thấy File Excel.");
                        return;
                    }

                    // Tìm và cập nhật hàng có STT tương ứng
                    bool rowFound = false;
                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (worksheet.Cells[row, 1].Text.Trim() == sttToUpdate)
                        {
                            // Cập nhật dữ liệu
                            worksheet.Cells[row, 2].Value = newName;
                            rowFound = true;
                            break;
                        }
                    }

                    if (!rowFound)
                    {
                        MessageBox.Show("STT Không Tồn Tại.");
                        return;
                    }

                    // Lưu file Excel
                    package.Save();
                }

                // Cập nhật DataGridView
                UpdateDataGridView();
                MessageBox.Show("Cập Nhật Thuốc Thành Công");
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
            DataTable reversedDataTable = ReverseDataTable(dataTable);
            dataGridView.DataSource = reversedDataTable;

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

        private void UpdateDataGridView()
        {
            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable dataTable = new DataTable();

                // Thêm các cột vào DataTable
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    dataTable.Columns.Add(worksheet.Cells[1, col].Text);
                }

                // Thêm các hàng vào DataTable
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                // Cập nhật DataGridView
                DataTable reversedDataTable = ReverseDataTable(dataTable);
                MedicinesDGV.DataSource = reversedDataTable;
            }
        }

        private DataTable ReverseDataTable(DataTable dataTable)
        {
            DataTable reversedTable = dataTable.Clone(); // Clone cấu trúc của DataTable
            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                reversedTable.ImportRow(dataTable.Rows[i]);
            }
            return reversedTable;
        }
    }
}
