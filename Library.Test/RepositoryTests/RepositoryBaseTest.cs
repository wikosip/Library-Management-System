using Library.Test.Helper;
using Library.Test.Configuration;
using Library.Repository.Interfaces;
using Library.Repository;

namespace Library.Test.RepositoryTests;

internal abstract class RepositoryBaseTest
{
    protected const int TestIdForUpdate = 1;
    protected const int TestIdForDelete = 2;
    protected IUnitOfWork _unitOfWork;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        DatabaseHelper.ClearDatabase();
        DatabaseHelper.SeedDatabase();
    }

    [SetUp]
    public void Setup()
    {
        _unitOfWork = UnitOfWorkFactory.Create(
            ConfigurationManager.ConnectionString);
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWork.Dispose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        DatabaseHelper.ClearDatabase();
    }
}