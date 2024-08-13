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
        string saveFilePath;
        private int selectedRowIndex = -1;

        public Order()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            filePath = dataPath.filePath;
            saveFilePath = dataPath.saveFilePath;
            // Gọi hàm để tải dữ liệu vào DataGridView
            SetUpLoadData(filePath,MedicinesDTGV);
        }

        private void SetUpLoadData(string filePath, DataGridView dtgv)
        {
            LoadExcelDataToDataGridView(filePath, dtgv);
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
            dataGridView.Columns[0].Width = 70;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.DefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);

            if (dataGridView.Columns.Count > 2)
            {
                dataGridView.Columns[2].Width = 100;
                dataGridView.Columns[3].Width = 150;
                dataGridView.Columns[4].Width = 150;
            }
        }

        private void AddBT_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn
            if (MedicinesDTGV.SelectedRows.Count > 0)
            {
                // Sắp xếp các hàng được chọn theo chỉ số hàng (row index)
                var selectedRows = MedicinesDTGV.SelectedRows.Cast<DataGridViewRow>()
                    .OrderBy(row => row.Index)
                    .ToList();

                // Tạo số thứ tự cho hàng mới (thực tế bạn có thể muốn tự động tăng số thứ tự này)
                int rowCount = OrderDTGV.Rows.Count + 1;

                foreach (DataGridViewRow selectedRow in selectedRows)
                {
                    // Lấy giá trị của cột thứ hai từ hàng được chọn
                    string columnValue = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;

                    // Thêm hàng mới vào OrderDTGV
                    OrderDTGV.Rows.Add(rowCount, columnValue, 0, "", "");

                    // Tăng số thứ tự cho hàng tiếp theo
                    rowCount++;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng thuốc!");
            }
        }

        private void Order_Resize(object sender, EventArgs e)
        {
            ResizeDTGV();
        }

        private void ResizeDTGV()
        {
            MedicinesDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            OrderDTGV.Dock = DockStyle.Right;
            // Cập nhật kích thước bảng bên trái
            MedicinesDTGV.Width = this.ClientSize.Width / 2 - 100;
            MedicinesDTGV.Height = this.ClientSize.Height;
            OrderDTGV.Width = this.ClientSize.Width / 2 + 80;
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
            // Khởi tạo OpenFileDialog để chọn file Excel
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Đặt tiêu đề cho hộp thoại
                openFileDialog.Title = "Chọn file Excel";
                // Đặt bộ lọc định dạng file
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                // Đặt đường dẫn mặc định cho hộp thoại lưu file
                openFileDialog.InitialDirectory = @"D:\PharmacyManager\PharmacyData\DatHang";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng chọn file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn file từ hộp thoại
                    string saveFilePath = openFileDialog.FileName;

                    // Thêm thời gian vào tên thư mục để tạo tên duy nhất mỗi lần chạy
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string outputDirectory = Path.Combine(Path.GetDirectoryName(saveFilePath), $"DatHang_{timestamp}");

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(outputDirectory))
                    {
                        Directory.CreateDirectory(outputDirectory);
                    }

                    using (var package = new ExcelPackage(new FileInfo(saveFilePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Giả sử làm việc với worksheet đầu tiên
                        int totalRows = worksheet.Dimension.Rows;
                        int rowsPerPage = 23;

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
            }
        }

        private Bitmap CreateBitmapFromWorksheet(ExcelWorksheet worksheet, int startRow, int endRow)
        {
            // Kích thước giấy A4 với độ phân giải 300 dpi
            int width = 2481; // 8.27 inches * 300 dpi
            int height = 3507; // 11.69 inches * 300 dpi

            // Chiều rộng cố định của cột 1 và cột 3
            int column1Width = 170;
            int column3Width = 350;
            // Chiều rộng của cột 4 và cột 5 (các cột mới)
            int column4Width = 450; 
            int column5Width = 450;

            int fixedRowHeight = 151;
            int numberOfRows = endRow - startRow + 1;

            // Tính chiều rộng của cột 2
            int column2Width = width - column1Width - column3Width - column4Width - column5Width;
            // Tính chiều cao tổng của hình ảnh dựa trên số lượng hàng và chiều cao hàng cố định
            int totalHeight = fixedRowHeight * numberOfRows;


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
                            else if (col == 3)
                            {
                                x = column1Width + column2Width;
                                columnWidth = column3Width;
                            }
                            else if (col == 4)
                            {
                                x = column1Width + column2Width + column3Width;
                                columnWidth = column4Width;
                            }
                            else // col == 5
                            {
                                x = column1Width + column2Width + column3Width + column4Width;
                                columnWidth = column5Width;
                            }

                            float y = (row - startRow) * fixedRowHeight;

                            // Vẽ văn bản vào hình ảnh
                            graphics.DrawString(cellValue, font, Brushes.Black, new RectangleF(x, y, columnWidth, fixedRowHeight));
                        }
                    }
                }

                // Vẽ đường kẻ bảng
                using (Pen pen = new Pen(Color.Black, 1)) // 1 pixel cho đường kẻ
                {
                    // Vẽ đường kẻ dọc
                    graphics.DrawLine(pen, column1Width, 0, column1Width, height); // Sau cột 1
                    graphics.DrawLine(pen, column1Width + column2Width, 0, column1Width + column2Width, height); // Sau cột 2
                    graphics.DrawLine(pen, column1Width + column2Width + column3Width, 0, column1Width + column2Width + column3Width, height); // Sau cột 3
                    graphics.DrawLine(pen, column1Width + column2Width + column3Width + column4Width, 0, column1Width + column2Width + column3Width + column4Width, height); // Sau cột 4

                    // Vẽ đường kẻ ngang
                    for (int row = startRow; row <= endRow; row++)
                    {
                        float y = (row - startRow) * fixedRowHeight;
                        graphics.DrawLine(pen, 0, y, width, y); // Trên cùng của từng hàng
                    }

                    // Vẽ đường kẻ dưới cùng của hàng cuối cùng
                    float lastRowY = (endRow - startRow + 1) * fixedRowHeight;
                    graphics.DrawLine(pen, 0, lastRowY, width, lastRowY);
                }
            }
            return bitmap;
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

        private void SaveBT_Click(object sender, EventArgs e)
        {
            // Khởi tạo SaveFileDialog
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Đặt tiêu đề cho hộp thoại
                saveFileDialog.Title = "Lưu file Excel";
                // Đặt bộ lọc định dạng file
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                // Đặt tên file mặc định
                saveFileDialog.FileName = "DatHang.xlsx";

                // Đặt đường dẫn mặc định cho hộp thoại lưu file
                saveFileDialog.InitialDirectory = @"D:\PharmacyManager\PharmacyData\DatHang";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng nhấn nút Save
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn file từ hộp thoại
                    string saveFilePath = saveFileDialog.FileName;
                    // Tạo một workbook và worksheet mới
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("DatHangTemp");

                        // Thêm tiêu đề cột
                        for (int i = 0; i < OrderDTGV.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = OrderDTGV.Columns[i].HeaderText;
                        }

                        // Thêm dữ liệu hàng
                        for (int rowIndex = 0; rowIndex < OrderDTGV.Rows.Count; rowIndex++)
                        {
                            for (int colIndex = 0; colIndex < OrderDTGV.Columns.Count; colIndex++)
                            {
                                var cellValue = OrderDTGV.Rows[rowIndex].Cells[colIndex].Value;

                                // Chuyển đổi đối tượng thành chuỗi
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

                        // Lưu workbook vào đường dẫn đã chỉ định
                        workbook.SaveAs(saveFilePath);
                    }
                    MessageBox.Show("Lưu File Đặt Hàng Thành Công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OpenBT_Click(object sender, EventArgs e)
        {
            // Khởi tạo OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Đặt tiêu đề cho hộp thoại
                openFileDialog.Title = "Chọn file Excel";
                // Đặt bộ lọc định dạng file
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                // Đặt đường dẫn mặc định cho hộp thoại lưu file
                openFileDialog.InitialDirectory = @"D:\PharmacyManager\PharmacyData\DatHang";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng chọn file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn file từ hộp thoại
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        OrderDTGV.Columns.Clear();
                        SetUpLoadData(filePath, OrderDTGV);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
