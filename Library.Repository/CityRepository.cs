using System.Data.Common;
using Dapper;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class CityRepository : RepositoryBase<City>, ICityRepository
{
    public CityRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }

    public IEnumerable<City> GetByCountryId(int countryId)
    {
        return _connection.Query<City>($"select * from Cities where IsDeleted = 0 and CountryId = {countryId}");
    }   
}