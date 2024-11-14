using System.IO;
using Newtonsoft.Json;

namespace FinalUserInterface
{
    public class SqlConnectionConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public bool TrustServerCertificate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static SqlConnectionConfig LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {filePath}");
            }

            string configJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<SqlConnectionConfig>(configJson);
        }

        public static void SaveConfig(string filePath, SqlConnectionConfig config)
        {
            string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, configJson);
        }
    }
}