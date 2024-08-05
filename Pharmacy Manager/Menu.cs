namespace Pharmacy_Manager
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void MedicinesBT_Click(object sender, EventArgs e)
        {
            Medicines medicines = new Medicines();
            medicines.Show();
        }

        private void OrderBT_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
        }

        private void RevenueBT_Click(object sender, EventArgs e)
        {
            Revenue revenue = new Revenue();
            revenue.Show();
        }
    }
}
