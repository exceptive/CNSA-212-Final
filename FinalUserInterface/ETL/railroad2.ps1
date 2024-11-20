# Define SQL connection using your provided connection string
$connString = "Server=10.200.10.93,1433;Database=Final212;User Id=Josiah;Password=CNSAcnsa1;"
$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = $connString

# Open the connection to the database
$sqlConnection.Open()

# Import the Excel module to read data from the Excel sheet
Import-Module ImportExcel

# Load the data from the 'TRAINS_DETAIL' sheet in Excel
$excelFilePath = "C:\Users\Evan Boley\source\repos\Test\Test\CY22.xlsx"  # Replace with your actual file path
$trainData = Import-Excel -Path $excelFilePath -WorksheetName "TRAINS_DETAIL"

# Prepare the SQL insert query for dbo.railroad table (no railroad_id in the insert)
$insertQuery = "INSERT INTO dbo.railroad (railroad_name) VALUES (@railroad_name)"

# Prepare the SQL command object
$command = $sqlConnection.CreateCommand()
$command.CommandText = $insertQuery

# Add parameter for railroad_name
$command.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@railroad_name", [System.Data.SqlDbType]::NVarChar, 255)))

# Loop through each row of train data from the Excel sheet
foreach ($row in $trainData) {
    $railroadName = $row.RAILROAD_NAME  # Ensure the column name matches in Excel

    # Set the parameter for each row
    $command.Parameters["@railroad_name"].Value = $railroadName
    
    # Execute the SQL insert command
    $command.ExecuteNonQuery()
}

# Close the SQL connection after inserting all rows
$sqlConnection.Close()

Write-Host "Data inserted successfully into dbo.railroad table."
