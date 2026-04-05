using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Library.Repository.Interfaces;

namespace Library.Repository;

public static class UnitOfWorkFactory
{
    public static IUnitOfWork Create(
        string connectionString,
        DatabaseType databaseType = DatabaseType.SqlServer)
    {
        return databaseType switch
        {
            DatabaseType.SqlServer => new UnitOfWork(new SqlConnection(connectionString)),
            DatabaseType.MySql => new UnitOfWork(new MySqlConnection(connectionString)),
            _ => throw new ArgumentException("Unsupported database type.")
        };
    }
}

public enum DatabaseType
{
    SqlServer,
    MySql,
    Oracle,
}
