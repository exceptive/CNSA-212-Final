using ClosedXML.Excel;  // Importing the ClosedXML library
using System;
using System.Data.SqlClient;

namespace ExcelToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Update this path to the actual location of your Excel file
            string excelFilePath = @"C:\Users\Evan Boley\source\repos\Final212\IncidentTableETL\Test3\CY22.xlsx";

            string connectionString = "Server=10.200.11.59,1433;Database=Final212;User Id=Josiah;Password=CNSAcnsa1;";

            // Use ClosedXML's XLWorkbook here, no more ambiguity
            using (var workbook = new XLWorkbook(excelFilePath))  // This refers to ClosedXML's XLWorkbook
            {
                var callsSheet = workbook.Worksheet("CALLS");
                var incidentCommonsSheet = workbook.Worksheet("incident_commons");
                var incidentDetailsSheet = workbook.Worksheet("INCIDENT_DETAILS");

                // Iterate through rows in the CALLS sheet
                foreach (var row in callsSheet.RowsUsed())
                {
                    // Extract values from the CALLS sheet
                    var seqnos = row.Cell("A").Value.ToString();
                    DateTime? dateTimeReceived = GetDateTimeFromCell(row.Cell("B"));
                    DateTime? dateTimeComplete = GetDateTimeFromCell(row.Cell("C"));
                    var callType = row.Cell("D").Value.ToString();
                    var responsibleCity = row.Cell("E").Value.ToString();
                    var responsibleState = row.Cell("H").Value.ToString();
                    var responsibleZip = row.Cell("I").Value.ToString();

                    // Extract description_of_incident, type_of_incident, and incident_cause from incident_commons sheet
                    var incidentCommonsRow = incidentCommonsSheet.Row(row.RowNumber());
                    var descriptionOfIncident = incidentCommonsRow.Cell("B").Value.ToString();
                    var typeOfIncident = incidentCommonsRow.Cell("C").Value.ToString();
                    var incidentCause = incidentCommonsRow.Cell("D").Value.ToString();

                    // Extract injury_count, fatality_count, and hospitalization_count from INCIDENT_DETAILS sheet
                    var incidentDetailsRow = incidentDetailsSheet.Row(row.RowNumber());
                    var injuryCount = GetIntFromCell(incidentDetailsRow.Cell("I"));
                    var fatalityCount = GetIntFromCell(incidentDetailsRow.Cell("L"));
                    var hospitalizationCount = GetIntFromCell(incidentDetailsRow.Cell("J"));

                    // Pull company_id, railroad_id, and incident_train_id from the DB
                    var companyId = GetCompanyId(connectionString); // Fetch company_id from company table
                    var railroadId = GetRailroadId(connectionString); // Fetch railroad_id from railroad table
                    var incidentTrainId = GetIncidentTrainId(connectionString); // Fetch incident_train_id from incident_train table

                    if (companyId == -1 || railroadId == -1 || incidentTrainId == -1)
                    {
                        Console.WriteLine("Missing data for company, railroad, or train. Skipping row.");
                        continue; // Skip this row if any ID is invalid
                    }

                    // Insert all values into the incident table
                    InsertIntoIncidentTable(seqnos, dateTimeReceived, dateTimeComplete, callType, responsibleCity,
                        responsibleState, responsibleZip, descriptionOfIncident, typeOfIncident, incidentCause,
                        injuryCount, hospitalizationCount, fatalityCount, companyId, railroadId, incidentTrainId, connectionString);
                }
            }

            Console.WriteLine("Data inserted successfully.");
        }

        // Insert data into the incident table
        static void InsertIntoIncidentTable(string seqnos, DateTime? dateTimeReceived, DateTime? dateTimeComplete,
            string callType, string responsibleCity, string responsibleState, string responsibleZip,
            string descriptionOfIncident, string typeOfIncident, string incidentCause,
            int? injuryCount, int? hospitalizationCount, int? fatalityCount,
            int companyId, int railroadId, int incidentTrainId, string connectionString)
        {
            try
            {
                // Open a connection to the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to insert data into the incident table (excluding the identity column)
                    string query = @"
                        INSERT INTO incident 
                        (seqnos, date_time_received, date_time_complete, call_type, responsible_city, 
                        responsible_state, responsible_zip, description_of_incident, type_of_incident, 
                        incident_cause, injury_count, hospitalization_count, fatality_count, 
                        company_id, railroad_id, incident_train_id)
                        VALUES 
                        (@seqnos, @dateTimeReceived, @dateTimeComplete, @callType, @responsibleCity, 
                        @responsibleState, @responsibleZip, @descriptionOfIncident, @typeOfIncident, 
                        @incidentCause, @injuryCount, @hospitalizationCount, @fatalityCount, 
                        @companyId, @railroadId, @incidentTrainId)";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@seqnos", seqnos);
                        cmd.Parameters.AddWithValue("@dateTimeReceived", (object)dateTimeReceived ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@dateTimeComplete", (object)dateTimeComplete ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@callType", callType);
                        cmd.Parameters.AddWithValue("@responsibleCity", responsibleCity);
                        cmd.Parameters.AddWithValue("@responsibleState", responsibleState);
                        cmd.Parameters.AddWithValue("@responsibleZip", responsibleZip);
                        cmd.Parameters.AddWithValue("@descriptionOfIncident", descriptionOfIncident);
                        cmd.Parameters.AddWithValue("@typeOfIncident", typeOfIncident);
                        cmd.Parameters.AddWithValue("@incidentCause", incidentCause);
                        cmd.Parameters.AddWithValue("@injuryCount", (object)injuryCount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@hospitalizationCount", (object)hospitalizationCount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@fatalityCount", (object)fatalityCount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@companyId", companyId);
                        cmd.Parameters.AddWithValue("@railroadId", railroadId);
                        cmd.Parameters.AddWithValue("@incidentTrainId", incidentTrainId);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the insert
                Console.WriteLine("Error inserting into incident table: " + ex.Message);
            }
        }

        // Method to get company_id from database
        static int GetCompanyId(string connectionString)
        {
            int companyId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 company_id FROM company";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out companyId))
                        {
                            return companyId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching company_id: " + ex.Message);
            }

            return -1;
        }

        // Method to get railroad_id from database
        static int GetRailroadId(string connectionString)
        {
            int railroadId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 railroad_id FROM railroad";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out railroadId))
                        {
                            return railroadId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching railroad_id: " + ex.Message);
            }

            return -1;
        }

        // Method to get incident_train_id from database
        static int GetIncidentTrainId(string connectionString)
        {
            int incidentTrainId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 train_id FROM incident_train";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out incidentTrainId))
                        {
                            return incidentTrainId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching incident_train_id: " + ex.Message);
            }

            return -1;
        }

        // Method to parse DateTime from a cell
        static DateTime? GetDateTimeFromCell(IXLCell cell)
        {
            if (cell.TryGetValue<DateTime>(out DateTime dateValue))
            {
                if (dateValue < new DateTime(1753, 1, 1) || dateValue > new DateTime(9999, 12, 31))
                {
                    Console.WriteLine($"Invalid Date in Cell {cell.Address}: {cell.Value}");
                    return null;
                }
                return dateValue;
            }
            return null;
        }

        // Method to parse Integer from a cell
        static int? GetIntFromCell(IXLCell cell)
        {
            if (cell.TryGetValue<int>(out int intValue))
            {
                return intValue;
            }
            return null;
        }
    }
}
