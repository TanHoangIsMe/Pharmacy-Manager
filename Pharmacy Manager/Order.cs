using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Pharmacy_Manager
{
    public partial class Order : Form
    {
        DataPath dataPath = new DataPath();
        string filePath;
        string saveFilePath;
        string currentFilePath = "";
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
            SetUpLoadData(filePath, MedicinesDTGV);
        }

        private void SetUpLoadData(string filePath, DataGridView dtgv)
        {
            LoadExcelDataToDataGridView(filePath, dtgv);
            ResizeDTGV();
            SettingDTGV(MedicinesDTGV);
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
                dataGridView.Columns[2].Width = 150;
                dataGridView.Columns[3].Width = 200;
            }
        }

        private void AddBT_Click(object sender, EventArgs e)
        {
            string filePath = currentFilePath;

            if(filePath == "")
            {
                MessageBox.Show("Vui lòng tạo đơn để thêm thuốc!");
                return;
            }

            // Kiểm tra nếu có hàng nào được chọn
            if (MedicinesDTGV.SelectedRows.Count > 0)
            {
                // Sắp xếp các hàng được chọn theo chỉ số hàng (row index)
                var selectedRows = MedicinesDTGV.SelectedRows.Cast<DataGridViewRow>()
                    .OrderBy(row => row.Index)
                    .ToList();

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet;
                    if (package.Workbook.Worksheets.Count == 0)
                        worksheet = package.Workbook.Worksheets.Add("OrderData");
                    else
                        worksheet = package.Workbook.Worksheets[0];

                    int lastRow = worksheet.Dimension?.Rows ?? 1; // Tìm dòng cuối cùng

                    foreach (DataGridViewRow selectedRow in selectedRows)
                    {
                        string columnValue = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
                        worksheet.Cells[lastRow + 1, 1].Value = lastRow; // STT
                        worksheet.Cells[lastRow + 1, 2].Value = columnValue; // Tên thuốc
                        worksheet.Cells[lastRow + 1, 3].Value = 0; // Số lượng mặc định 0
                        worksheet.Cells[lastRow + 1, 4].Value = ""; // Ghi chú trống

                        lastRow++;
                    }

                    package.Save();
                }

                // Load lại dữ liệu từ Excel vào DataGridView
                LoadExcelDataToDataGridView(filePath,OrderDTGV);
                SettingDTGV(OrderDTGV);
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
            MedicinesDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            OrderDTGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            OrderDTGV.Location = new Point(this.ClientSize.Width / 2, OrderDTGV.Location.Y);
            MedicinesDTGV.Width = this.ClientSize.Width / 2 - 20;
            MedicinesDTGV.Height = this.ClientSize.Height;
            OrderDTGV.Width = this.ClientSize.Width / 2 - 10;
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
            string filePath = currentFilePath;

            if (filePath == "")
            {
                MessageBox.Show("Không tìm thấy thuốc để xóa");
                return;
            }

            if (OrderDTGV.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa các hàng đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        // Lấy danh sách index của các hàng cần xóa, sắp xếp theo thứ tự giảm dần
                        List<int> selectedIndexes = OrderDTGV.SelectedRows.Cast<DataGridViewRow>()
                            .Select(row => row.Index + 2) // +2 vì Excel tính từ 1 và hàng tiêu đề là dòng 1
                            .OrderByDescending(rowIndex => rowIndex) // Sắp xếp giảm dần để tránh lệch index khi xóa
                            .ToList();

                        foreach (int rowIndex in selectedIndexes)
                        {
                            worksheet.DeleteRow(rowIndex);
                        }

                        // Cập nhật lại số thứ tự (STT)
                        int lastRow = worksheet.Dimension?.Rows ?? 0; // Số dòng hiện tại sau khi xóa
                        for (int i = 2; i <= lastRow; i++) // Bắt đầu từ dòng 2 để tránh tiêu đề
                        {
                            worksheet.Cells[i, 1].Value = i - 1; // Cập nhật STT mới
                        }

                        package.Save();
                    }

                    // Load lại dữ liệu sau khi xóa
                    LoadExcelDataToDataGridView(filePath, OrderDTGV);
                    SettingDTGV(OrderDTGV);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng để xóa!");
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
            string filePath = currentFilePath;

            if (filePath == "")
            {
                MessageBox.Show("Không tìm thấy file để xuất ảnh");
                return;
            }

            try
            {
                // Thêm thời gian vào tên thư mục để tạo thư mục lưu ảnh mới mỗi lần xuất
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                string outputDirectory = Path.Combine(Path.GetDirectoryName(filePath), $"Đặt Hàng_{timestamp}");

                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(outputDirectory);

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Làm việc với worksheet đầu tiên
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

                MessageBox.Show("Tạo Hình Thành Công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("explorer.exe", outputDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int fixedRowHeight = 151;
            int numberOfRows = endRow - startRow + 1;

            // Tính chiều rộng của cột 2
            int column2Width = width - column1Width - column3Width - column4Width;
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
                            else
                            {
                                x = column1Width + column2Width + column3Width;
                                columnWidth = column4Width;
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
            string filePath = currentFilePath;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Không tìm thấy file dữ liệu để lưu!");
                return;
            }

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Giả sử dữ liệu nằm ở Sheet đầu tiên

                int rowCount = OrderDTGV.Rows.Count;

                for (int i = 0; i < rowCount; i++)
                {
                    DataGridViewRow row = OrderDTGV.Rows[i];

                    worksheet.Cells[i + 2, 1].Value = row.Cells[0].Value; // STT
                    worksheet.Cells[i + 2, 2].Value = row.Cells[1].Value; // Tên thuốc
                    worksheet.Cells[i + 2, 3].Value = row.Cells[2].Value; // Số lượng
                    worksheet.Cells[i + 2, 4].Value = row.Cells[3].Value; // Ghi chú
                }

                package.Save();
            }

            MessageBox.Show("Dữ liệu đã được lưu vào file Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        LoadExcelDataToDataGridView(filePath, OrderDTGV);
                        SettingDTGV(OrderDTGV);
                        currentFilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SearchOrderTB_TextChanged(object sender, EventArgs e)
        {
            // Lấy nội dung tìm kiếm từ TextBox
            string searchText = SearchOrderTB.Text.Trim().ToLower();

            // Kiểm tra nếu OrderDTGV đang dùng DataTable làm DataSource
            if (OrderDTGV.DataSource is DataTable dataTable)
            {
                string columnName = dataTable.Columns[1].ColumnName; // Lấy tên cột thứ 2
                dataTable.DefaultView.RowFilter = $"[{columnName}] LIKE '%{searchText}%'";
            }
            else
            {
                foreach (DataGridViewRow row in OrderDTGV.Rows)
                {
                    // Bỏ ẩn toàn bộ hàng trước khi lọc
                    row.Visible = true;

                    // Lấy giá trị của cột cần tìm kiếm (cột "Tên thuốc")
                    if (row.Cells[1].Value != null)
                    {
                        string cellValue = row.Cells[1].Value.ToString().ToLower();

                        // Nếu không chứa nội dung tìm kiếm thì ẩn hàng
                        if (!cellValue.Contains(searchText))
                        {
                            row.Visible = false;
                        }
                    }
                }
            }
        }

        private void CreateBT_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn tạo đơn hàng mới không?", "Tạo Đơn Mới", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Đường dẫn thư mục lưu file
                    string folderPath = @"D:\PharmacyManager\PharmacyData\DatHang\";

                    // Kiểm tra thư mục có tồn tại không, nếu không thì tạo mới
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Tạo tên file với ngày giờ để tránh trùng lặp
                    string fileName = "Đặt Hàng_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".xlsx";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Tạo file Excel mới
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("OrderData");

                        // Ghi một số dữ liệu mẫu vào file Excel (có thể thay đổi)
                        worksheet.Cells[1, 1].Value = "STT";
                        worksheet.Cells[1, 2].Value = "Tên Thuốc";
                        worksheet.Cells[1, 3].Value = "Số Lượng";
                        worksheet.Cells[1, 4].Value = "Giá Tiền";

                        // Lưu file Excel
                        File.WriteAllBytes(filePath, package.GetAsByteArray());
                    }

                    LoadExcelDataToDataGridView(filePath, OrderDTGV);
                    SettingDTGV(OrderDTGV);
                    currentFilePath = filePath;
                    MessageBox.Show($"Đơn đã được tạo thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tạo file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
