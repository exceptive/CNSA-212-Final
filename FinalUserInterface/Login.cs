using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FinalUserInterface
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string configFilePath = Path.Combine(Application.StartupPath, "config.json");
            SqlConnectionConfig config = SqlConnectionConfig.LoadConfig(configFilePath);

            config.Username = textBoxUsername.Text;
            config.Password = textBoxPassword.Text;

            string testConnectionString = $"Server={config.Server};Database={config.Database};User Id={config.Username};Password={config.Password};TrustServerCertificate={config.TrustServerCertificate};";

            if (TestConnection(testConnectionString))
            {
                AppConfig.ConnectionString = testConnectionString;

                SqlConnectionConfig.SaveConfig(configFilePath, config);

                this.DialogResult = DialogResult.OK;
                this.Hide();

                Selection selectionForm = new Selection(config.Username, config.Password);
                selectionForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TestConnection(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}