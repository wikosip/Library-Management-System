using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Library.Test.Configuration;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class AuthorRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewAuthorToDatabase()
    {
        IAuthorRepository repository = _unitOfWork.AuthorRepository!;
        Author newAuthor = new()
        {
            FirstName = "Test1",
            LastName = "TC1",
            NationalityId = 1
        };

        int id = repository!.Insert(newAuthor);
        Author? insertedAuthor = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedAuthor, Is.Not.Null);
        Assert.That(insertedAuthor!.FirstName, Is.EqualTo(newAuthor.FirstName));
        Assert.That(insertedAuthor.LastName, Is.EqualTo(newAuthor.LastName));
        Assert.That(insertedAuthor!.BirthDate, Is.EqualTo(newAuthor.BirthDate));
    }

    [Test]
    public void Insert_ShouldNotAddNewAuthorToDatabase()
    {
        IAuthorRepository repository = _unitOfWork.AuthorRepository!;
        Author newAuthor = new()
        {
            FirstName = "Test2",
            LastName = null!,
            BirthDate = null,
            DeathDate = null
        };

        Assert.Throws<SqlException>(() => repository.Insert(newAuthor));
    }

    [Test]
    public void Update_ShouldUpdateNewAuthorToDatabase()
    {
        IAuthorRepository repository = _unitOfWork.AuthorRepository!;
        Author? existingAuthor = repository.GetById(TestIdForUpdate);
        if (existingAuthor == null)
        {
            Assert.Fail($"Author with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        existingAuthor.FirstName = $"Updated{existingAuthor.FirstName}";
        existingAuthor.BirthDate = new DateTime(1920, 03, 02);
        existingAuthor.DeathDate = new DateTime(1977, 07, 06);
        repository.Update(existingAuthor);
        Author? updatedAuthor = repository.GetById(TestIdForUpdate);

        Assert.That(updatedAuthor, Is.Not.Null);
        Assert.That(updatedAuthor!.FirstName, Is.EqualTo(updatedAuthor.FirstName));
        Assert.That(updatedAuthor!.BirthDate, Is.EqualTo(existingAuthor.BirthDate));
        Assert.That(updatedAuthor!.DeathDate, Is.EqualTo(existingAuthor.DeathDate));
    }

    [Test]
    public void Update_ShouldNotUpdateNewAuthorWithInvalidData()
    {
        IAuthorRepository repository = _unitOfWork.AuthorRepository!;
        var existingAuthor = repository.GetById(TestIdForUpdate);
        existingAuthor!.FirstName = null!;

        Assert.Throws<SqlException>(() => repository.Update(existingAuthor));
    }

    [Test]
    public void Delete_ShouldRemoveAuthorFromDatabase()
    {
        IAuthorRepository repository = _unitOfWork.AuthorRepository!;
        Author? existingAuthor = repository.GetById(TestIdForDelete);
        if (existingAuthor == null)
        {
            Assert.Fail($"Author with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        Author? deletedAuthor = repository.GetById(TestIdForDelete);

        Assert.That(deletedAuthor, Is.Null);
    }
}