using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Test.RepositoryTests;

internal class RepositoryTransactionTests : RepositoryBaseTest
{
    [Test]
    public void Transaction_ShouldCommitChanges()
    {
        ICountryRepository repository = _unitOfWork.CountryRepository;
        HashSet<int> expectedCountryIds = new HashSet<int>();
        HashSet<int> insertedCountryIds = new HashSet<int>();

        _unitOfWork.BeginTransaction();
        InsertTestCountries(repository, insertedCountryIds);
        _unitOfWork.Commit();
        ExpectedCountries(repository, insertedCountryIds, expectedCountryIds);

        Assert.That(insertedCountryIds, Is.EquivalentTo(expectedCountryIds));
    }

    [Test]
    public void Transaction_ShouldRollbackChanges()
    {
        ICountryRepository repository = _unitOfWork.CountryRepository;
        HashSet<int> notExpectedCountryIds = new HashSet<int>();
        HashSet<int> insertedCountryIds = new HashSet<int>();

        _unitOfWork.BeginTransaction();
        InsertTestCountries(repository, insertedCountryIds);
        _unitOfWork.Rollback();
        ExpectedCountries(repository, insertedCountryIds, notExpectedCountryIds);

        Assert.That(notExpectedCountryIds, Is.Empty);
    }

    private static void ExpectedCountries(
        ICountryRepository repository,
        ISet<int> insertedCountries,
        ISet<int> expectedCountries)
    {
        foreach(int id in insertedCountries)
        {
            Country? country = repository.GetById(id);
            if (country != null)
                expectedCountries.Add(id);
        }
    }

    private static void InsertTestCountries(ICountryRepository repository, ISet<int> insertedCountries)
    {
        Random random = new();
        for (int i = 0; i < 5; i++)
        {
            int randomNumber = random.Next(10000, 100000);
            Country newCountry = new()
            {
                Name = $"TestCountry{randomNumber}",
                IsoName = $"{randomNumber}"
            };
            int id = repository.Insert(newCountry);
            insertedCountries.Add(id);
        }
    }
}