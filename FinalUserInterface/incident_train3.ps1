# Define SQL connection
$connString = "Server=10.200.10.93,1433;Database=Final212;User Id=Josiah;Password=CNSAcnsa1;"
$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = $connString
$sqlConnection.Open()

# Load the data from the 'TRAINS_DETAIL' sheet in Excel
$trainData = Import-Excel -Path $excelFilePath -WorksheetName "TRAINS_DETAIL"

# Loop through each row of train data from the Excel sheet
foreach ($row in $trainData) {
    # Extract values from the Excel sheet
    $nameNumber = $row.TRAIN_NAME_NUMBER
    $trainType = $row.TRAIN_TYPE
    $railroadName = $row.RAILROAD_NAME

    # Debugging: print the railroad_name value for each row
    Write-Host "Processing Train: $nameNumber - Railroad Name: '$railroadName'"

    # Lookup the railroad_id from the dbo.railroad table based on railroad_name
    $railroadIdQuery = "SELECT railroad_id FROM dbo.railroad WHERE railroad_name = @railroadName"
    $command = $sqlConnection.CreateCommand()
    $command.CommandText = $railroadIdQuery

    # Add the railroad_name parameter to the command
    $command.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@railroadName", [Data.SqlDbType]::NVarChar, 255))).Value = $railroadName

    # Execute the query and retrieve the railroad_id
    $railroadId = $command.ExecuteScalar()

    # Check if the railroad_id was found
    if ($railroadId -eq $null) {
        Write-Host "Skipping Train $nameNumber due to missing Railroad ID for '$railroadName'"
        continue
    }

    # Set the parameters for each row, but omit train_id (identity column is auto-generated)
    $insertQuery = "INSERT INTO dbo.incident_train (name_number, train_type, railroad_id) VALUES (@name_number, @train_type, @railroad_id)"
    $insertCommand = $sqlConnection.CreateCommand()
    $insertCommand.CommandText = $insertQuery
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@name_number", [Data.SqlDbType]::NVarChar, 255))).Value = $nameNumber
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@train_type", [Data.SqlDbType]::NVarChar, 50))).Value = $trainType
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@railroad_id", [Data.SqlDbType]::Int))).Value = $railroadId

    # Execute the SQL insert command
    try {
        $insertCommand.ExecuteNonQuery()
        Write-Host "Inserted Train: $nameNumber successfully."
    }
    catch {
        Write-Host "Error inserting Train: $nameNumber - $($_.Exception.Message)"
    }
}
