using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FinalUserInterface
{
    public class SqlConnectionConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public bool TrustServerCertificate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

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


            string configFilePath = Path.Combine(Application.StartupPath, "config.json");
            SqlConnectionConfig config = LoadConfig(configFilePath);


            string testConnectionString = $"Server={config.Server};Database={config.Database};User Id={Username};Password={Password};TrustServerCertificate={config.TrustServerCertificate};";

            if (TestConnection(testConnectionString))
            {

                AppConfig.Username = Username;
                AppConfig.Password = Password;
                AppConfig.ConnectionString = testConnectionString;


                config.Username = Username;
                config.Password = Password;
                SaveConfig(configFilePath, config);


                this.DialogResult = DialogResult.OK;
                this.Hide();


                Selection selectionForm = new Selection(Username, Password);
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


        private static SqlConnectionConfig LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {filePath}");
            }

            string configJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<SqlConnectionConfig>(configJson);
        }


        private static void SaveConfig(string filePath, SqlConnectionConfig config)
        {
            string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, configJson);
        }

        private bool ValidateCredentials(string username, string password)
        {
            return true;
        }
    }
}