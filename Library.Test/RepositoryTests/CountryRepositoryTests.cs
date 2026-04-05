using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

[TestFixture]
internal class CountryRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewCountryWithValidData()
    {
        // Arrange
        ICountryRepository repository = _unitOfWork.CountryRepository;
        Country newCountry = new()
        {
            Name = $"Test{Guid.NewGuid()}",
            IsoName = "TC"
        };

        // Act
        var id = repository.Insert(newCountry);
        var insertedCountry = repository.GetById(id);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedCountry, Is.Not.Null);
        Assert.That(insertedCountry!.Name, Is.EqualTo(newCountry.Name));
        Assert.That(insertedCountry.IsoName, Is.EqualTo(newCountry.IsoName));
    }

    [Test]
    public void Insert_ShouldNotAddNewCountryWithInvalidData()
    {
        // Arrange
        ICountryRepository repository = _unitOfWork.CountryRepository;
        Country newCountry = new()
        {
            Name = $"Test{Guid.NewGuid()}",
            IsoName = null!
        };

        // Act & Assert
        Assert.Throws<SqlException>(() => repository.Insert(newCountry));
    }

    [Test]
    public void Update_ShouldUpdateNewCountryWithValidData()
    {
        // Arrange
        ICountryRepository repository = _unitOfWork.CountryRepository;

        // Act
        var existingCountry = repository.GetById(TestIdForUpdate);
        if (existingCountry == null)
        {
            Assert.Fail($"Country with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingCountry.Name = $"Updated{existingCountry.Name}";
        repository.Update(existingCountry);
        var updatedCountry = repository.GetById(TestIdForUpdate);

        // Assert
        Assert.That(updatedCountry, Is.Not.Null);
        Assert.That(updatedCountry!.Name, Is.EqualTo(existingCountry.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateNewCountryWithInvalidData()
    {
        ICountryRepository repository = _unitOfWork.CountryRepository;
        var newCountry = repository.GetById(TestIdForUpdate);
        if(newCountry == null)
        {
            Assert.Fail($"Country with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        newCountry.IsoName = null!;

        Assert.Throws<SqlException>(() => repository.Update(newCountry));
    }

    [Test]
    public void Delete_ShouldRemoveCountryFromDatabase()
    {
        // Arrange
        ICountryRepository repository = _unitOfWork.CountryRepository;

        // Act
        var existingCountry = repository.GetById(TestIdForDelete);
        if (existingCountry == null)
        {
            Assert.Fail($"Country with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        var deletedCountry = repository.GetById(TestIdForDelete);

        // Assert
        Assert.That(deletedCountry, Is.Null);
    }
}