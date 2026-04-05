using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class BookLoanRepository : RepositoryBase<BookLoan>, IBookLoanRepository
{
    public BookLoanRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}