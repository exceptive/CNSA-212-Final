using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Maintenance : Form
    {
        public Maintenance()
        {
            InitializeComponent();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            string newPassword = textBox2.Text;
            string configFilePath = Path.Combine(Application.StartupPath, "config.json");


            SqlConnectionConfig config = SqlConnectionConfig.LoadConfig(configFilePath);
            string username = config.Username;


            string connectionString = AppConfig.ConnectionString;

            string query = $@"
ALTER LOGIN [{username}]
WITH PASSWORD = @newPassword";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newPassword", newPassword);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();


                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password successfully changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to change the password.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}