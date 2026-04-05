using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}