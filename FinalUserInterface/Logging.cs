using System;
using System.IO;
using System.Windows.Forms;

public static class Logger
{
    private static readonly string LogDirectory = "../../../";
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "logfile.txt");
    private static readonly string ErrorLogFilePath = Path.Combine(LogDirectory, "error_log.txt");

    static Logger()
    {

        if (!Directory.Exists(LogDirectory))
        {
            Directory.CreateDirectory(LogDirectory);
        }
    }

    public static void LogDatabaseCommand(string commandText, string parameters)
    {
        try
        {

            using (StreamWriter writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now}] Command Executed: {commandText}");
                writer.WriteLine($"[{DateTime.Now}] Parameters: {parameters}");
                writer.WriteLine(new string('-', 50));
            }


            Console.WriteLine($"Log written to: {LogFilePath}");
        }
        catch (Exception ex)
        {

            LogError($"Error Logging Command: {ex.Message}", ex.StackTrace);
        }
    }

    public static void LogError(string message, string stackTrace = null)
    {
        try
        {

            using (StreamWriter writer = new StreamWriter(ErrorLogFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now}] Error: {message}");
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    writer.WriteLine($"Stack Trace: {stackTrace}");
                }
                writer.WriteLine(new string('-', 50));
            }


            Console.WriteLine($"Error log written to: {ErrorLogFilePath}");
        }
        catch (Exception ex)
        {

            MessageBox.Show($"Critical Error: Unable to write logs. Details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void LogTestMessage()
    {
        try
        {
            LogDatabaseCommand("TEST QUERY", "TEST PARAMETERS");
            LogError("This is a test error for logging verification.", "No stack trace available for test.");
            MessageBox.Show("Test log entries created successfully!", "Logger Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Logger test failed: {ex.Message}", "Logger Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}