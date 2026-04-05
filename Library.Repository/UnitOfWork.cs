using System.Data.Common;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly DbConnection _connection;
    private DbTransaction? _transaction;
    private IAuthorRepository? _authorRepository;
    private IBookInstanceRepository? _bookInstanceRepository;
    private IBookLoanRepository? _bookLoanRepository;
    private IBookRepository? _bookRepository;
    private ICityRepository? _cityRepository;
    private IConditionRepository? _conditionRepository;
    private ICountryRepository? _countryRepository;
    private ICustomerMembershipRepository? _customerMembershipRepository;
    private ICustomerRepository? _customerRepository;
    private IEmployeeRepository? _employeeRepository;
    private IGenreRepository? _genreRepository;
    private ILanguageRepository? _languageRepository;
    private IMembershipTypeRepository? _membershipTypeRepository;
    private INationalityRepository? _nationalityRepository;
    private IPaymentRepository? _paymentRepository;
    private IPermissionRepository? _permissionRepository;
    private IPositionRepository? _positionRepository;
    private IPublisherRepository? _publisherRepository;
    private IRoleRepository? _roleRepository;

    public UnitOfWork(DbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        if (_connection.State != System.Data.ConnectionState.Open)
            _connection.Open();
    }

    public IAuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_connection, () => _transaction);
    public IBookInstanceRepository BookInstanceRepository => _bookInstanceRepository ??= new BookInstanceRepository(_connection, () => _transaction);
    public IBookLoanRepository BookLoanRepository => _bookLoanRepository ??= new BookLoanRepository(_connection, () => _transaction);
    public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_connection, () => _transaction);
    public ICityRepository CityRepository => _cityRepository ??= new CityRepository(_connection, () => _transaction);
    public IConditionRepository ConditionRepository => _conditionRepository ??= new ConditionRepository(_connection, () => _transaction);
    public ICountryRepository CountryRepository => _countryRepository ??= new CountryRepository(_connection, () => _transaction);
    public ICustomerMembershipRepository CustomerMembershipRepository => _customerMembershipRepository ??= new CustomerMembershipRepository(_connection, () => _transaction);
    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_connection, () => _transaction);
    public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_connection, () => _transaction);
    public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_connection, () => _transaction);
    public ILanguageRepository LanguageRepository => _languageRepository ??= new LanguageRepository(_connection, () => _transaction);
    public IMembershipTypeRepository MembershipTypeRepository => _membershipTypeRepository ??= new MembershipTypeRepository(_connection, () => _transaction);
    public INationalityRepository NationalityRepository => _nationalityRepository ??= new NationalityRepository(_connection, () => _transaction);
    public IPaymentRepository PaymentRepository => _paymentRepository ??= new PaymentRepository(_connection, () => _transaction);
    public IPermissionRepository PermissionRepository => _permissionRepository ??= new PermissionRepository(_connection, () => _transaction);
    public IPositionRepository PositionRepository => _positionRepository ??= new PositionRepository(_connection, () => _transaction);
    public IPublisherRepository PublisherRepository => _publisherRepository ??= new PublisherRepository(_connection, () => _transaction);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_connection, () => _transaction);

    public void BeginTransaction()
    {
        if (_transaction != null)
            throw new InvalidOperationException("A transaction is already in progress.");
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction in progress.");
        
        try
        {
            _transaction.Commit();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction in progress.");

        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (!disposing)
            return;

        _transaction?.Dispose();
        _connection.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}