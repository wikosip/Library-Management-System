using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class PublisherRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewPublisherToDatabase()
    {
        IPublisherRepository repository = _unitOfWork.PublisherRepository;
        Publisher newPublisher = new()
        {
            Name = "TestPublisher"
        };

        int id = repository.Insert(newPublisher);
        Publisher? insertedPublisher = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedPublisher, Is.Not.Null);
        Assert.That(insertedPublisher!.Name, Is.EqualTo(newPublisher.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewPublisherToDatabase()
    {
        IPublisherRepository repository = _unitOfWork.PublisherRepository;
        Publisher newPublisher = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newPublisher));
    }

    [Test]
    public void Update_ShouldUpdatePublisherInDatabase()
    {
        IPublisherRepository repository = _unitOfWork.PublisherRepository;

        Publisher? existingPublisher = repository.GetById(TestIdForUpdate);
        if (existingPublisher == null)
        {
            Assert.Fail($"Publisher with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingPublisher.Name = $"Updated{existingPublisher.Name}";
        repository.Update(existingPublisher);
        Publisher? updatedPublisher = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPublisher, Is.Not.Null);
        Assert.That(updatedPublisher!.Name, Is.EqualTo(existingPublisher.Name));
    }

    [Test]
    public void Update_ShouldNotUpdatePublisherInDatabase()
    {
        IPublisherRepository repository = _unitOfWork.PublisherRepository;
        Publisher? existingPublisher = repository.GetById(TestIdForUpdate);
        existingPublisher!.Name = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingPublisher));
    }

    [Test]
    public void Delete_ShouldRemovePublisherFromDatabase()
    {
        IPublisherRepository repository = _unitOfWork.PublisherRepository;

        Publisher? existingPublisher = repository.GetById(TestIdForDelete);
        if (existingPublisher == null)
        {
            Assert.Fail($"Publisher with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        Publisher? deletedPublisher = repository.GetById(TestIdForDelete);

        Assert.That(deletedPublisher, Is.Null);
    }
}
