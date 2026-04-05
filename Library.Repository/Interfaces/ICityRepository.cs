using Library.DTO;

namespace Library.Repository.Interfaces;

public interface ICityRepository : IRepository<City>
{
    IEnumerable<City> GetByCountryId(int countryId);
}