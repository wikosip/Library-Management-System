using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.RepositoryTests;

internal class MembershipTypeRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewMembershipTypeToDatabase()
    {
        IMembershipTypeRepository repository = _unitOfWork.MembershipTypeRepository;
        MembershipType newMembershipType = new()
        {
            Name = "TestPublisher",
            DurationDay = 1,
            Price = 9.99m
        };

        int id = repository.Insert(newMembershipType);
        MembershipType? insertedMembershipType = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedMembershipType, Is.Not.Null);
        Assert.That(insertedMembershipType!.Name, Is.EqualTo(newMembershipType.Name));
        Assert.That(insertedMembershipType.DurationDay, Is.EqualTo(newMembershipType.DurationDay));
        Assert.That(insertedMembershipType.Price, Is.EqualTo(newMembershipType.Price));
    }

    [Test]
    public void Insert_ShouldNotAddNewMembershipTypeToDatabase()
    {
        IMembershipTypeRepository repository = _unitOfWork.MembershipTypeRepository;
        MembershipType newMembershipType = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newMembershipType));
    }

    [Test]
    public void Update_ShouldUpdateMembershipTypeInDatabase()
    {
        IMembershipTypeRepository repository = _unitOfWork.MembershipTypeRepository;

        MembershipType? existingMembershipType = repository.GetById(TestIdForUpdate);
        if (existingMembershipType == null)
        {
            Assert.Fail($"Publisher with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingMembershipType.Name = $"Updated{existingMembershipType.Name}";
        existingMembershipType.DurationDay += 10;
        existingMembershipType.Price += 5.00m;
        repository.Update(existingMembershipType);
        MembershipType? updatedPublisher = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPublisher, Is.Not.Null);
        Assert.That(updatedPublisher!.Name, Is.EqualTo(existingMembershipType.Name));
        Assert.That(updatedPublisher.DurationDay, Is.EqualTo(existingMembershipType.DurationDay));
        Assert.That(updatedPublisher.Price, Is.EqualTo(existingMembershipType.Price));
    }

    [Test]
    public void Update_ShouldNotUpdateMembershipTypeInDatabase()
    {
        IMembershipTypeRepository repository = _unitOfWork.MembershipTypeRepository;
        MembershipType? existingMembershipType = repository.GetById(TestIdForUpdate);
        existingMembershipType!.Name = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingMembershipType));
    }

    [Test]
    public void Delete_ShouldRemoveMembershipTypeFromDatabase()
    {
        IMembershipTypeRepository repository = _unitOfWork.MembershipTypeRepository;

        MembershipType? existingMembershipType = repository.GetById(TestIdForDelete);
        if (existingMembershipType == null)
        {
            Assert.Fail($"MembershipType with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        MembershipType? deletedMembershipType = repository.GetById(TestIdForDelete);

        Assert.That(deletedMembershipType, Is.Null);
    }
}
