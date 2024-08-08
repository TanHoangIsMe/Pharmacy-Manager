using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;
using System.Drawing.Imaging;


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
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
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
                int rowCount = OrderDTGV.Rows.Count + 1;

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
            MedicinesDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MedicinesDTGV.Width = this.ClientSize.Width / 2; // Chiếm 50% chiều ngang ban đầu
            MedicinesDTGV.Height = this.ClientSize.Height; // Chiều cao ban đầu
            OrderDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            OrderDTGV.Width = this.ClientSize.Width / 2;
            OrderDTGV.Height = this.ClientSize.Height;
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

        private void ExportBT_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to prompt the user to select a save location
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save Excel File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Path where the file will be saved
                    string filePath = saveFileDialog.FileName;

                    // Create a new workbook and worksheet
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet =
                            workbook.Worksheets.Add("DatHangTemp");

                        // Add column headers
                        for (int i = 0; i < OrderDTGV.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = OrderDTGV.Columns[i].HeaderText;
                        }

                        // Add row data
                        for (int rowIndex = 0; rowIndex < OrderDTGV.Rows.Count; rowIndex++)
                        {
                            for (int colIndex = 0; colIndex < OrderDTGV.Columns.Count; colIndex++)
                            {
                                var cellValue = OrderDTGV.Rows[rowIndex].Cells[colIndex].Value;

                                // Convert the object to a string
                                if (cellValue != null)
                                {
                                    worksheet.Cell(rowIndex + 2, colIndex + 1).Value = cellValue.ToString();
                                }
                                else
                                {
                                    worksheet.Cell(rowIndex + 2, colIndex + 1).Value = string.Empty;
                                }
                            }
                        }

                        // Save the workbook to the specified file path
                        workbook.SaveAs(filePath);
                    }

                    MessageBox.Show("Tạo File Excel Thành Công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConvertExcelToPng(string excelFilePath)
        {
            // Thêm thời gian vào tên thư mục để tạo tên duy nhất mỗi lần chạy
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputDirectory = Path.Combine(Path.GetDirectoryName(excelFilePath), $"DatHang_{timestamp}");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Giả sử làm việc với worksheet đầu tiên
                int totalRows = worksheet.Dimension.Rows;
                int rowsPerPage = 25;

                for (int startRow = 1; startRow <= totalRows; startRow += rowsPerPage)
                {
                    int endRow = Math.Min(startRow + rowsPerPage - 1, totalRows);
                    Bitmap bitmap = CreateBitmapFromWorksheet(worksheet, startRow, endRow);
                    string outputPath = Path.Combine(outputDirectory, $"Hình_{startRow / rowsPerPage + 1}.png");
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }
            MessageBox.Show("Tạo Hình Thành Công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Bitmap CreateBitmapFromWorksheet(ExcelWorksheet worksheet, int startRow, int endRow)
        {
            // Kích thước giấy A4 với độ phân giải 300 dpi
            int width = 2481; // 8.27 inches * 300 dpi
            int height = 3507; // 11.69 inches * 300 dpi

            // Chiều rộng cố định của cột 1 và cột 3
            int column1Width = 150;
            int column3Width = 350;

            // Tính chiều rộng của cột 2
            int column2Width = width - column1Width - column3Width;
            int rowHeight = height / (endRow - startRow + 1); // Kích thước của mỗi hàng

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                // Sử dụng font để vẽ văn bản
                using (Font font = new Font("Arial", 39, FontStyle.Bold))
                {
                    // Vẽ thông tin từ worksheet vào hình ảnh
                    for (int row = startRow; row <= endRow; row++)
                    {
                        for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                        {
                            string cellValue = worksheet.Cells[row, col].Text;

                            // Tính toán vị trí và kích thước của từng ô
                            float x;
                            float columnWidth;

                            if (col == 1)
                            {
                                x = 0;
                                columnWidth = column1Width;
                            }
                            else if (col == 2)
                            {
                                x = column1Width;
                                columnWidth = column2Width;
                            }
                            else // col == 3
                            {
                                x = column1Width + column2Width;
                                columnWidth = column3Width;
                            }

                            float y = (row - startRow) * rowHeight;

                            // Vẽ văn bản vào hình ảnh
                            graphics.DrawString(cellValue, font, Brushes.Black, new RectangleF(x, y, columnWidth, rowHeight));
                        }
                    }
                }

                // Vẽ đường kẻ bảng
                using (Pen pen = new Pen(Color.Black, 1)) // 1 pixel cho đường kẻ
                {
                    // Vẽ đường kẻ dọc
                    graphics.DrawLine(pen, column1Width, 0, column1Width, height); // Sau cột 1
                    graphics.DrawLine(pen, column1Width + column2Width, 0, column1Width + column2Width, height); // Sau cột 2

                    // Vẽ đường kẻ ngang
                    for (int row = startRow; row <= endRow; row++)
                    {
                        float y = (row - startRow) * rowHeight;
                        graphics.DrawLine(pen, 0, y, width, y); // Trên cùng của từng hàng
                    }

                    // Vẽ đường kẻ dưới cùng của hàng cuối cùng
                    float lastRowY = (endRow - startRow + 1) * rowHeight;
                    graphics.DrawLine(pen, 0, lastRowY, width, lastRowY);
                }
            }
            return bitmap;
        }

        private void ImageBT_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ConvertExcelToPng(filePath);
                }
            }
        }

        private void SearchTB_TextChanged(object sender, EventArgs e)
        {
            // Lấy nội dung tìm kiếm từ TextBox
            string searchText = SearchTB.Text.Trim().ToLower();

            // Kiểm tra xem DataGridView có DataSource là DataTable không
            if (MedicinesDTGV.DataSource is DataTable dataTable)
            {
                // Tạo bộ lọc cho DataTable
                // Lấy tên cột thứ hai từ DataTable
                string columnName = dataTable.Columns[1].ColumnName;

                // Tạo biểu thức lọc cho DataTable
                string filterExpression = $"[{columnName}] LIKE '%{searchText}%'";

                // Áp dụng bộ lọc cho DataTable
                dataTable.DefaultView.RowFilter = filterExpression;
            }
        }
    }
}
