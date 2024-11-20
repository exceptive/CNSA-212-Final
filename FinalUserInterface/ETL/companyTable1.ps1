# Import the Excel data, replacing null values with "Unknown" where necessary
$excelData = Import-Excel -Path "C:\Users\Evan Boley\source\repos\Test\Test\CY22.xlsx" -WorksheetName "CALLS" |
    Select-Object @{Name='company_name'; Expression={ if ($_.RESPONSIBLE_COMPANY -eq $null) { "Unknown" } else { $_.'RESPONSIBLE_COMPANY' }}}, 
                  @{Name='org_type'; Expression={ if ($_.RESPONSIBLE_ORG_TYPE -eq $null) { "Unknown" } else { $_.'RESPONSIBLE_ORG_TYPE' }}}

# Convert the data to a DataTable for SqlBulkCopy
$dataTable = New-Object System.Data.DataTable
$companyNameCol = $dataTable.Columns.Add("company_name", [string])
$orgTypeCol = $dataTable.Columns.Add("org_type", [string])

# Populate the DataTable with Excel data
foreach ($row in $excelData) {
    $dataRow = $dataTable.NewRow()
    $dataRow["company_name"] = $row.company_name
    $dataRow["org_type"] = $row.org_type
    $dataTable.Rows.Add($dataRow)
}

# Set up the SQL connection and bulk copy
$connectionString = "Server=10.200.11.59,1433;Database=Final212;User Id=Josiah;Password=CNSAcnsa1;"
$sqlBulkCopy = New-Object Data.SqlClient.SqlBulkCopy($connectionString)
$sqlBulkCopy.DestinationTableName = "dbo.company"

# Map the columns explicitly
$sqlBulkCopy.ColumnMappings.Add("company_name", "company_name")
$sqlBulkCopy.ColumnMappings.Add("org_type", "org_type")

# Perform the bulk insert
$sqlBulkCopy.WriteToServer($dataTable)
