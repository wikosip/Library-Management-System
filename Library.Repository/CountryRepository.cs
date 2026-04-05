using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository
{
    internal sealed class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)      {
            
        }
    }
}
