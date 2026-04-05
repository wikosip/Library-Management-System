using Library.DTO;

namespace Library.Repository.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    IEnumerable<Author> GetByBirthDate(DateTime from, DateTime to);
}