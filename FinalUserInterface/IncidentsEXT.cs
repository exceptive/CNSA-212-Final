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
    public partial class IncidentsEXT : Form
    {
        public IncidentsEXT(string IncidentsEXT)
        {
            InitializeComponent();
            LoadIncidents(IncidentsEXT);
        }

        private void LoadIncidents(string IncidentsEXT)
        {
            string conectionString = AppConfig.ConnectionString;

            if (!string.IsNullOrEmpty(conectionString))
            {
                string query = "SELECT * FROM Incident WHERE railroad_incident_id = @IncidentsEXT";

                try
                {
                    using (var connection = new SqlConnection(conectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@IncidentsEXT", IncidentsEXT);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No incidents found for this company.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
   