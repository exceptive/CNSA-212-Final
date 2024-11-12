using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Railroads : Form
    {
        private string username;

        public Railroads()
        {
            InitializeComponent();
        }

        // Constructor accepting username
        public Railroads(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Railroads_Load(object sender, EventArgs e)
        {
            // Use the connection string stored in AppConfig
            string connectionString = AppConfig.ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                LoadData(connectionString);
            }
            else
            {
                MessageBox.Show("Connection string is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM test_table";  // Example query

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Display data in DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}