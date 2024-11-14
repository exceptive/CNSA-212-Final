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
        }

        private void Railroads_Load(object sender, EventArgs e)
        {
            string connectionString = AppConfig.ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {

                if (TestConnection(connectionString))
                {

                    LoadData(connectionString);
                }
                else
                {
                    MessageBox.Show("Failed to connect to the database.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM railroad";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("No data found in the table.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}