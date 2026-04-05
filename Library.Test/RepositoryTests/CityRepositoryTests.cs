using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

[TestFixture]
internal class CityRepositoryTests : RepositoryBaseTest
{

    [Test]
    public void Insert_ShouldAddNewCityToDatabase()
    {
        ICityRepository repository = _unitOfWork.CityRepository;
        City newCity = new()
        {
            Name = "Test1",
            CountryId = 1,
            IsoName = "TC"
        };

        int id = repository.Insert(newCity);
        City? insertedCity = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedCity, Is.Not.Null);
        Assert.That(insertedCity!.Name, Is.EqualTo(newCity.Name));
        Assert.That(insertedCity.IsoName, Is.EqualTo(newCity.IsoName));
    }

    [Test]
    public void Insert_ShouldNotAddNewCityToDatabase()
    {
        ICityRepository repository = _unitOfWork.CityRepository;
        City newCity = new()
        {
            Name = "Test2",
            IsoName = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newCity));
    }

    [Test]
    public void Update_ShouldUpdateNewCityToDatabase()
    {
        ICityRepository repository = _unitOfWork.CityRepository;

        City? existingCity = repository.GetById(TestIdForUpdate);
        if (existingCity == null)
        {
            Assert.Fail($"City with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        existingCity.Name = $"Updated{existingCity.Name}";
        repository.Update(existingCity);
        City? updatedCity = repository.GetById(TestIdForUpdate);

        Assert.That(updatedCity, Is.Not.Null);
        Assert.That(updatedCity!.Name, Is.EqualTo(existingCity.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateNewCityWithInvalidData()
    {
        ICityRepository repository = _unitOfWork.CityRepository;
        var newCity = repository.GetById(TestIdForUpdate);
        newCity!.Name = null!;

        Assert.Throws<SqlException>(() => repository.Update(newCity));
    }

    [Test]
    public void Delete_ShouldRemoveCityFromDatabase()
    {
        ICityRepository repository = _unitOfWork.CityRepository;

        City? existingCity = repository.GetById(TestIdForDelete);
        if (existingCity == null)
        {
            Assert.Fail($"City with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(2);
        City? deletedCity = repository.GetById(TestIdForDelete);

        Assert.That(deletedCity, Is.Null);
    }
}