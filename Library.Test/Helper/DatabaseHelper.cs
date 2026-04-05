using Library.Test.Configuration;
using Microsoft.Data.SqlClient;

namespace Library.Test.Helper
{
    internal static class DatabaseHelper
    {
        public static void ClearDatabase()
        {
            using SqlConnection connection = new(ConfigurationManager.ConnectionString);
            connection.Open();
            using SqlCommand command = new("usp_ClearDatabase", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.ExecuteNonQuery();
        }

        public static void SeedDatabase()
        {
            using SqlConnection connection = new(ConfigurationManager.ConnectionString);
            connection.Open();
            using SqlCommand seedCommand = new("usp_SeedDatabase", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            seedCommand.ExecuteNonQuery();
        }
    }
}
