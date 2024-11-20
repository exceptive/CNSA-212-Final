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
    public partial class RailroadsIncident : Form
    {
        public RailroadsIncident(string railroadID)
        {
            InitializeComponent();
            LoadIncidents(railroadID);
        }

        private void LoadIncidents(string railroadID)
        {
            string connectionString = AppConfig.ConnectionString;
            if (!String.IsNullOrEmpty(connectionString))
                {
                    string query = "SELECT * FROM Incident WHERE railroad_id = @railroadID";
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@railroadID", railroadID);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No incidents found for this railroad.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading incidents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Connection string is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }
}
}
