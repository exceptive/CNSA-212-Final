namespace FinalUserInterface
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            // I still have not added password functionality but the forms now lead to other forms
            // This will need to be changed later when we understand the password login
            Selection selectionForm = new Selection();

            selectionForm.Show();

        }
    }
}
