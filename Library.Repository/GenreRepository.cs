using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}