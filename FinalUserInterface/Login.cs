using System;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Login : Form
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Login()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Username = textBoxUsername.Text;
            Password = textBoxPassword.Text;

            if (ValidateCredentials(Username, Password))
            {
                // Store credentials and connection string in AppConfig
                AppConfig.Username = Username;
                AppConfig.Password = Password;
                AppConfig.ConnectionString = $"Server=DESKTOP-770QVUT;Database=Final212;User Id={Username};Password={Password};TrustServerCertificate=True;";

                // Hide login form and open the selection form
                this.DialogResult = DialogResult.OK;
                this.Hide();

                // Pass both username and password to the Selection form
                Selection selectionForm = new Selection(Username, Password);
                selectionForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Implement validation logic (e.g., check against a list or database)
            return true;  // Assume credentials are valid for now
        }
    }
}