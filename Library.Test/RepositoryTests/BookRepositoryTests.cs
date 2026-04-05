using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class BookRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewBookWithValidData()
    {
        IBookRepository repository = _unitOfWork.BookRepository!;
        Book newbook = new()
        {
            GenreId = 1,
            PublisherId = 1,
            Title = "Test",
            Isbn = "Test",
            PublicationYear = 1950,
            PageCount = 50
        };

        var id = repository.Insert(newbook);
        var insertedBook = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedBook, Is.Not.Null);
        Assert.That(insertedBook!.Title, Is.EqualTo(newbook.Title));
        Assert.That(insertedBook.Isbn, Is.EqualTo(newbook.Isbn));
        Assert.That(insertedBook!.PublicationYear, Is.EqualTo(newbook.PublicationYear));
        Assert.That(insertedBook.PageCount, Is.EqualTo(newbook.PageCount));
    }

    [Test]
    public void Insert_ShouldNotAddNewBookWithInvalidData()
    {
        IBookRepository repository = _unitOfWork.BookRepository!;
        Book newBook = new()
        {
            GenreId = 1,
            PublisherId = 1,
            Title = null!,
            Isbn = null!,
            PublicationYear = 11111,
            PageCount = 50,
            BookId = 1
        };

        Assert.Throws<SqlException>(() => repository.Insert(newBook));
    }

    [Test]
    public void Update_ShouldUpdateNewBookWithValidData()
    {
        IBookRepository repository = _unitOfWork.BookRepository!;

        var existingBook = repository.GetById(TestIdForUpdate);
        if (existingBook == null)
        {
            Assert.Fail($"Book with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingBook.Title = $"Updated{existingBook.Title}";
        repository.Update(existingBook);
        var updatedBook = repository.GetById(TestIdForUpdate);

        Assert.That(updatedBook, Is.Not.Null);
        Assert.That(updatedBook!.Title, Is.EqualTo(existingBook.Title));
    }

    [Test]
    public void Update_ShouldNotUpdateNewBookWithInvalidData()
    {
        IBookRepository repository = _unitOfWork.BookRepository!;
        var newBook = repository.GetById(TestIdForUpdate);
        newBook!.Isbn = null!;

        Assert.Throws<SqlException>(() => repository.Update(newBook));
    }

    [Test]
    public void Delete_ShouldRemoveBookFromDatabase()
    {
        IBookRepository repository = _unitOfWork.BookRepository!;

        var existingBook = repository.GetById(TestIdForDelete);
        if (existingBook == null)
        {
            Assert.Fail($"Book with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        var deletedBook = repository.GetById(TestIdForDelete);

        Assert.That(deletedBook, Is.Null);
    }
}
