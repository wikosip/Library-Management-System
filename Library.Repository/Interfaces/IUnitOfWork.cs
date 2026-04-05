using Org.BouncyCastle.Asn1.BC;

namespace Library.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAuthorRepository? AuthorRepository { get; }
    IBookRepository? BookRepository { get; }
    IBookLoanRepository BookLoanRepository { get; }
    IBookInstanceRepository BookInstanceRepository { get; }
    ICityRepository CityRepository { get; }
    IConditionRepository ConditionRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICustomerMembershipRepository CustomerMembershipRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IGenreRepository GenreRepository { get; }
    ILanguageRepository LanguageRepository { get; }
    IMembershipTypeRepository MembershipTypeRepository { get; }
    INationalityRepository NationalityRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IPermissionRepository PermissionRepository { get; }
    IPositionRepository PositionRepository { get; }
    IPublisherRepository PublisherRepository { get; }
    IRoleRepository RoleRepository { get; }
    void BeginTransaction();
    void Commit();
    void Dispose(bool disposing);
    void Rollback();
}