using System.Data.Common;
using Dapper;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }

    public IEnumerable<Author> GetByBirthDate(DateTime from, DateTime to)
    {
        string query = "select * from Author where BirthDate between @from and @to and IsDeleted = 0";
        return _connection.Query<Author>(query, new { from, to });
    }
}