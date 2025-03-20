
namespace ClinicManagementSystemFinal
{
    public partial class FrontPage : Form
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage hm = new HomePage();
            hm.Show();
   
        }
    }
}
