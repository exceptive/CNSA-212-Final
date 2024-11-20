using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Railroads : Form
    {
        public Railroads()
        {
            InitializeComponent();
            this.Load += Railroads_Load;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        private void Railroads_Load(object sender, EventArgs e)
        {
            string connectionString = AppConfig.ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                Logger.LogDatabaseCommand("Attempting to connect to the database", $"Connection string: {connectionString}");

                if (TestConnection(connectionString))
                {
                    Logger.LogDatabaseCommand("Connection successful", "Loading data");
                    LoadData(connectionString);
                }
                else
                {
                    Logger.LogError("Database connection failed", "Connection test returned false");
                    MessageBox.Show("Failed to connect to the database.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Logger.LogError("Connection string not set", "Connection string is null or empty");
                MessageBox.Show("Connection string is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.LogDatabaseCommand("Database connection opened", "TestConnection successful");
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
            string query = "SELECT * FROM railroad";

            try
            {
                Logger.LogDatabaseCommand("Loading data", query);

                using (var connection = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                        Logger.LogDatabaseCommand("Data successfully loaded", $"Rows retrieved: {dataTable.Rows.Count}");
                    }
                    else
                    {
                        Logger.LogDatabaseCommand("No data found in table", "Table: railroad");
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
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var RailroadID = dataGridView1.Rows[e.RowIndex].Cells["railroad_id"].Value.ToString();


                RailroadsIncident incidentsForm = new RailroadsIncident(RailroadID);
                incidentsForm.ShowDialog(); 
            }
        }
    }
}