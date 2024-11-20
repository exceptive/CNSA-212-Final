using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Incidents : Form
    {
        private void button1_Click(object sender, EventArgs e)
        {
            Logger.LogDatabaseCommand("Opening New Incident Form", "No parameters");
            NewIncident NewIncidentForm = new NewIncident();
            NewIncidentForm.Show();
        }
        public Incidents()
        {
            InitializeComponent();
            this.Load += Incidents_Load;
        }
        private void Incidents_Load(object sender, EventArgs e)
        {
            string connectionString = AppConfig.ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                Logger.LogDatabaseCommand("Checking connection string validity", "Connection string is set");
                if (TestConnection(connectionString))
                {
                    Logger.LogDatabaseCommand("Connection successful", "Loading data");
                    LoadData(connectionString);
                }
                else
                {
                    Logger.LogError("Database connection failed.", "Verify connection string and database status.");
                    MessageBox.Show("Failed to connect to the database.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Logger.LogError("Connection string is not set.", "Check AppConfig or configuration file.");
                MessageBox.Show("Connection string is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TestConnection(string connectionString)
        {
            try
            {
                Logger.LogDatabaseCommand("Testing database connection", "Opening SQL connection");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.LogDatabaseCommand("Database connection test passed", "Connection opened successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Connection test failed: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Connection failed: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM incident";

            try
            {
                Logger.LogDatabaseCommand("Executing data load query", $"Query: {query}");
                using (var connection = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        Logger.LogDatabaseCommand("Data load successful", $"{dataTable.Rows.Count} rows loaded");
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        Logger.LogDatabaseCommand("No data found in table", "Incident table is empty");
                        MessageBox.Show("No data found in the table.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading data: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Error loading data: {ex.Message}", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
