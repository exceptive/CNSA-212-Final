using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class NewIncident : Form
    {
        public NewIncident()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateTimeReceived = textBox1.Text;
            string dateTimeCompleted = textBox2.Text;
            string callType = textBox3.Text;
            string responsibleState = textBox4.Text;
            string responsibleZip = textBox5.Text;
            string descriptionOfIncident = textBox6.Text;
            string typeOfIncident = textBox7.Text;
            string incidentCause = textBox8.Text;
            string injuryCount = textBox9.Text;
            string hospitalizationCount = textBox10.Text;
            string fatalityCount = textBox11.Text;

            string connectionString = AppConfig.ConnectionString;

            string query = @"
INSERT INTO incident (
        date_time_received, date_Time_Complete, call_Type, responsible_State, responsible_Zip,
        description_Of_Incident, type_Of_Incident, incident_Cause, injury_Count, hospitalization_Count, fatality_Count)
        Values(@dateTimeReceived, @dateTimeCompleted, @callType, @responsibleState, @responsibleZip,
        @descriptionOfIncident, @typeOfIncident, @incidentCause, @injuryCount, @hospitalizationCount, @fatalityCount);";

            try
            {
                Logger.LogDatabaseCommand("Preparing to insert new incident record", query);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@DateTimeReceived", dateTimeReceived);
                        command.Parameters.AddWithValue("@DateTimeCompleted", dateTimeCompleted);
                        command.Parameters.AddWithValue("@CallType", callType);
                        command.Parameters.AddWithValue("@ResponsibleState", responsibleState);
                        command.Parameters.AddWithValue("@ResponsibleZip", responsibleZip);
                        command.Parameters.AddWithValue("@DescriptionOfIncident", descriptionOfIncident);
                        command.Parameters.AddWithValue("@TypeOfIncident", typeOfIncident);
                        command.Parameters.AddWithValue("@IncidentCause", incidentCause);
                        command.Parameters.AddWithValue("@InjuryCount", injuryCount);
                        command.Parameters.AddWithValue("@HospitalizationCount", hospitalizationCount);
                        command.Parameters.AddWithValue("@FatalityCount", fatalityCount);


                        connection.Open();
                        Logger.LogDatabaseCommand("Database connection opened", "Executing insert command");
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        Logger.LogDatabaseCommand("Database connection closed", $"Rows affected: {rowsAffected}");


                        if (rowsAffected > 0)
                        {
                            Logger.LogDatabaseCommand("Insert successful", "Incident record added");
                            MessageBox.Show("Incident record successfully added to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Logger.LogError("Insert failed", "No rows affected by the query");
                            MessageBox.Show("Failed to add incident record.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                Logger.LogError($"SQL Error: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                Logger.LogError($"Unexpected Error: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}