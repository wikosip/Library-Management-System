using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

[TestFixture]
internal class LanguageRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewLanguageWithValidData()
    {
        ILanguageRepository repository = _unitOfWork.LanguageRepository;
        Language newLanguage = new()
        {
            Name = "Test1"
        };

        var id = repository.Insert(newLanguage);
        var insertedLanguage = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedLanguage, Is.Not.Null);
        Assert.That(insertedLanguage!.Name, Is.EqualTo(newLanguage.Name));
    }
    [Test]
    public void Insert_ShouldNotAddNewLanguageWithInvalidData()
    {
        ILanguageRepository repository = _unitOfWork.LanguageRepository;
        Language newLanguage = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newLanguage));
    }
    [Test]
    public void Update_ShouldUpdateLanguageWithValidData()
    {
        ILanguageRepository repository = _unitOfWork.LanguageRepository;

        var existingLanguage = repository.GetById(TestIdForUpdate);
        if (existingLanguage == null)
        {
            Assert.Fail($"Language with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingLanguage.Name = $"Updated{existingLanguage.Name}";
        repository.Update(existingLanguage);
        var updatedLanguage = repository.GetById(TestIdForUpdate);

        Assert.That(updatedLanguage, Is.Not.Null);
        Assert.That(updatedLanguage!.Name, Is.EqualTo(existingLanguage.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateLanguageWithInvalidData()
    {
        ILanguageRepository repository = _unitOfWork.LanguageRepository;
        var existingLanguage = repository.GetById(TestIdForUpdate);
        existingLanguage!.Name = null!;

        Assert.Throws<SqlException>(() => repository.Update(existingLanguage));
    }

    [Test]
    public void Delete_ShouldRemoveLanguageFromDatabase()
    {
        ILanguageRepository repository = _unitOfWork.LanguageRepository;

        var existingLanguage = repository.GetById(TestIdForDelete);
        if (existingLanguage == null)
        {
            Assert.Fail($"Language with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        var deletedLanguage = repository.GetById(TestIdForDelete);
        Assert.That(deletedLanguage, Is.Null);

    }
}
