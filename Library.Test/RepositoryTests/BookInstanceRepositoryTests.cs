using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class BookInstanceRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewBookInstanceToDatabase()
    {
        IBookInstanceRepository repository = _unitOfWork.BookInstanceRepository;
        BookInstance newBookInstance = new()
        {
            BookId = 1,
            ConditionId = 1
        };
        int id = repository.Insert(newBookInstance);
        BookInstance? insertedBookInstance = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedBookInstance, Is.Not.Null);
        Assert.That(insertedBookInstance!.ConditionId, Is.EqualTo(newBookInstance.ConditionId));
        Assert.That(insertedBookInstance.BookId, Is.EqualTo(newBookInstance.BookId));
    }

    [Test]
    public void Insert_ShouldNotAddNewBookInstanceToDatabase()
    {
        IBookInstanceRepository repository = _unitOfWork.BookInstanceRepository;
        BookInstance newBookInstance= new()
        {
            BookId = 0,
            ConditionId = 0
        };

        Assert.Throws<SqlException>(() => repository.Insert(newBookInstance));
    }

    [Test]
    public void Update_ShouldUpdateNewBookInstanceToDatabase()
    {
        IBookInstanceRepository repository = _unitOfWork.BookInstanceRepository;

        var existingBookInstance = repository.GetById(TestIdForUpdate);
        if (existingBookInstance == null)
        {
            Assert.Fail($"Book Instance with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        existingBookInstance.BookId = 2;
        repository.Update(existingBookInstance);
        var updatedBookInstance = repository.GetById(TestIdForUpdate);

        Assert.That(updatedBookInstance, Is.Not.Null);
        Assert.That(updatedBookInstance!.BookId, Is.EqualTo(existingBookInstance.BookId));
    }

    [Test]
    public void Update_ShouldNotUpdateNewBookInstanceWithInvalidData()
    {
        IBookInstanceRepository repository = _unitOfWork.BookInstanceRepository;
        var existingBookInstance = repository.GetById(TestIdForUpdate);
        existingBookInstance!.BookId = 0;

        Assert.Throws<SqlException>(() => repository.Update(existingBookInstance));
    }

    [Test]
    public void Delete_ShouldRemoveBookInstanceFromDatabase()
    {
        IBookInstanceRepository repository = _unitOfWork.BookInstanceRepository;

        var existingBookInstance = repository.GetById(TestIdForDelete);
        if (existingBookInstance == null)
        {
            Assert.Fail($"Book Instance with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        var deletedAuthor = repository.GetById(TestIdForDelete);

        Assert.That(deletedAuthor, Is.Null);
    }
}