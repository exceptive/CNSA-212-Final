using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

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
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            AppConfig.Username = textBoxUsername.Text;
            AppConfig.Password = textBoxPassword.Text;


            string connectionString = $"Server=DESKTOP-77OQVUT;Database=Final212;User Id={username};Password={password};TrustServerCertificate=True;";
            AppConfig.ConnectionString = connectionString;
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT PasswordHash, Salt FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        Logger.LogDatabaseCommand(query, $"@Username = {username}");

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                string storedHashedPassword = reader["PasswordHash"].ToString();
                                string storedSalt = reader["Salt"].ToString();


                                Console.WriteLine($"Stored Hashed Password: {storedHashedPassword}");
                                Console.WriteLine($"Stored Salt: {storedSalt}");

                                string hashedInputPassword = HashedPassword(password, storedSalt);


                                Console.WriteLine($"Hashed Input Password: {hashedInputPassword}");


                                if (hashedInputPassword == storedHashedPassword)
                                {
                                    Logger.LogDatabaseCommand("Login Successful", $"User: {username}");
                                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                    this.Hide();
                                    Selection selectionForm = new Selection(username, password);
                                    selectionForm.ShowDialog();
                                    this.Close();
                                }
                                else
                                {
                                   Logger.LogError($"Failed login attempt for user '{username}'");
                                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                             Logger.LogError($"Failed login attempt for user '{username}' - User not found.");
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Login error for user '{username}': {ex.Message}");
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string HashedPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {

                string saltedPassword = salt + password;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
