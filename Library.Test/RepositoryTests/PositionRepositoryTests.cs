using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.RepositoryTests;

internal class PositionRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewPositionToDatabase()
    {
        IPositionRepository repository = _unitOfWork.PositionRepository;
        Position newPosition = new()
        {
            Name = "TestPosition",
            Description = "TestDescription",
        };

        int id = repository.Insert(newPosition);
        Position? insertedPosition = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedPosition, Is.Not.Null);
        Assert.That(insertedPosition!.Name, Is.EqualTo(newPosition.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewPermissionToDatabase()
    {
        IPositionRepository repository = _unitOfWork.PositionRepository;
        Position newPosition = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newPosition));
    }

    [Test]
    public void Update_ShouldUpdatePositionInDatabase()
    {
        IPositionRepository repository = _unitOfWork.PositionRepository;

        Position? existingPosition = repository.GetById(TestIdForUpdate);
        if (existingPosition == null)
        {
            Assert.Fail($"Position with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingPosition.Name = $"Updated{existingPosition.Name}";
        repository.Update(existingPosition);
        Position? updatedPosition = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPosition, Is.Not.Null);
        Assert.That(updatedPosition!.Name, Is.EqualTo(existingPosition.Name));
    }

    [Test]
    public void Update_ShouldNotUpdatePositionInDatabase()
    {
        IPositionRepository repository = _unitOfWork.PositionRepository;
        Position? existingPosition = repository.GetById(TestIdForUpdate);
        existingPosition!.Name = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingPosition));
    }

    [Test]
    public void Delete_ShouldRemovePositionFromDatabase()
    {
        IPositionRepository repository = _unitOfWork.PositionRepository;

        Position? existingPosition = repository.GetById(TestIdForDelete);
        if (existingPosition == null)
        {
            Assert.Fail($"Position with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        Position? deletedPosition = repository.GetById(TestIdForDelete);

        Assert.That(deletedPosition, Is.Null);
    }
}
