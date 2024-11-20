# Define SQL connection
$connString = "Server=10.200.10.93,1433;Database=Final212;User Id=Josiah;Password=CNSAcnsa1;"
$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = $connString
$sqlConnection.Open()

# Load the data from the 'DERAILED_UNITS' sheet in Excel
$trainCarData = Import-Excel -Path $excelFilePath -WorksheetName "DERAILED_UNITS"

# Loop through each row of train car data from the Excel sheet
foreach ($row in $trainCarData) {
    # Extract values from the Excel sheet
    $carNumber = $row.CAR_NUMBER
    $carContent = $row.CAR_CONTENT
    $positionInTrain = $row.POSITION_IN_TRAIN
    $carType = $row.DERAILED_TYPE  # Assuming this is the column with the car type

    # Extract the corresponding train_id from incident_train (this assumes the train_number is unique)
    $incidentTrainIdQuery = "SELECT train_id FROM dbo.incident_train WHERE name_number = @nameNumber"
    $command = $sqlConnection.CreateCommand()
    $command.CommandText = $incidentTrainIdQuery

    # Add the name_number parameter to find the correct incident_train_id
    $command.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@nameNumber", [Data.SqlDbType]::NVarChar, 255))).Value = $row.TRAIN_NAME_NUMBER

    # Execute the query and retrieve the incident_train_id
    $incidentTrainId = $command.ExecuteScalar()

    # Check if the incident_train_id was found
    if ($incidentTrainId -eq $null) {
        Write-Host "Skipping Train Car $carNumber due to missing Incident Train ID for Train Number '$($row.TRAIN_NAME_NUMBER)'"
        continue
    }

    # Set the parameters for each row, but omit train_car_id (identity column is auto-generated)
    $insertQuery = "INSERT INTO dbo.incident_train_car (car_number, car_content, position_in_train, car_type, incident_train_id) 
                    VALUES (@car_number, @car_content, @position_in_train, @car_type, @incident_train_id)"
    $insertCommand = $sqlConnection.CreateCommand()
    $insertCommand.CommandText = $insertQuery

    # Add parameters correctly to the insert command
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@car_number", [Data.SqlDbType]::NVarChar, 50))).Value = $carNumber
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@car_content", [Data.SqlDbType]::NVarChar, 255))).Value = $carContent
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@position_in_train", [Data.SqlDbType]::Int))).Value = $positionInTrain
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@car_type", [Data.SqlDbType]::NVarChar, 50))).Value = $carType
    $insertCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@incident_train_id", [Data.SqlDbType]::Int))).Value = $incidentTrainId

    # Execute the SQL insert command
    try {
        $insertCommand.ExecuteNonQuery()
        Write-Host "Inserted Train Car: $carNumber successfully."
    }
    catch {
        Write-Host "Error inserting Train Car: $carNumber - $($_.Exception.Message)"
    }
}
