using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class NationalityRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewNationalityWithValidData()
    {
        INationalityRepository repository = _unitOfWork.NationalityRepository;
        Nationality newNationality = new()
        {
            Name = "Test1"
        };

        var id = repository.Insert(newNationality);
        var insertedNationality = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedNationality, Is.Not.Null);
        Assert.That(insertedNationality!.Name, Is.EqualTo(newNationality.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewNationalityWithInvalidData()
    {
        INationalityRepository repository = _unitOfWork.NationalityRepository;
        Nationality newCountry = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newCountry));
    }

    [Test]
    public void Update_ShouldUpdateNewNationalityWithValidData()
    {
        INationalityRepository repository = _unitOfWork.NationalityRepository;

        var existingNationality = repository.GetById(TestIdForUpdate);
        if (existingNationality == null)
        {
            Assert.Fail($"Nationality with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingNationality.Name = $"Updated{existingNationality.Name}";
        repository.Update(existingNationality);
        var updatedNationality = repository.GetById(TestIdForUpdate);

        Assert.That(updatedNationality, Is.Not.Null);
        Assert.That(updatedNationality!.Name, Is.EqualTo(updatedNationality.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateNewNationalityWithInvalidData()
    {
        INationalityRepository repository = _unitOfWork.NationalityRepository;
        var newNationality = repository.GetById(TestIdForUpdate);
        newNationality!.Name = null!;

        Assert.Throws<SqlException>(() => repository.Update(newNationality));
    }

    [Test]
    public void Delete_ShouldRemoveNationalityFromDatabase()
    {
        INationalityRepository repository = _unitOfWork.NationalityRepository;

        var existingNationality = repository.GetById(TestIdForDelete);
        if (existingNationality == null)
        {
            Assert.Fail($"Country with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        var deletedNationality = repository.GetById(TestIdForDelete);

        Assert.That(deletedNationality, Is.Null);
    }
}
