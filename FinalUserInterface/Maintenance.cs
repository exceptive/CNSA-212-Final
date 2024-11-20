using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
            string username = AppConfig.Username;
            string password = AppConfig.Password;
            string newPassword = textBoxNewPassword.Text;

            string connectionString = AppConfig.ConnectionString;

            try
            {
                Logger.LogDatabaseCommand("Attempting to change password", $"Username: {username}, New Password: {newPassword}");
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(newPassword, salt);


                string updateQuery = @"
UPDATE Users 
SET Salt = @Salt, PasswordHash = @PasswordHash
WHERE Username = @Username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Salt", salt);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        Logger.LogDatabaseCommand("Executing password update query", updateQuery);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            string alterLoginQuery = $@"
ALTER LOGIN [{username}]
WITH PASSWORD = '{newPassword}'";
                            string formattedAlterQuery = string.Format(alterLoginQuery, username, newPassword);

                            using (SqlCommand alterCommand = new SqlCommand(formattedAlterQuery, connection))
                            {
                                alterCommand.ExecuteNonQuery();
                            }

                            Logger.LogDatabaseCommand("Password successfully updated", $"Rows affected: {rowsAffected}");
                            MessageBox.Show("Password successfully changed. Restart required.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Logger.LogError("Password change failed", "User not found");
                            MessageBox.Show("Password change failed. User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"Database error: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }


        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {

                string saltedPassword = salt + password;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}