using Library.DTO;
using Library.Repository.Interfaces;
using System.Data.Common;
using System.Transactions;

namespace Library.Repository;

internal sealed class BookInstanceRepository : RepositoryBase<BookInstance>, IBookInstanceRepository
{
    public BookInstanceRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}