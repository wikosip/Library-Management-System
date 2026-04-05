using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Security.Policy;

namespace Library.Test.RepositoryTests;

internal class RoleRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewRoleToDatabase()
    {
        IRoleRepository repository = _unitOfWork.RoleRepository;
        Role newRole = new()
        {
            Name = "TestPublisher",
        };

        int id = repository.Insert(newRole);
        Role? insertedRole = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedRole, Is.Not.Null);
        Assert.That(insertedRole!.Name, Is.EqualTo(newRole.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewRoleToDatabase()
    {
        IRoleRepository repository = _unitOfWork.RoleRepository;
        Role newRole = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newRole));
    }

    [Test]
    public void Update_ShouldUpdateRoleInDatabase()
    {
        IRoleRepository repository = _unitOfWork.RoleRepository;

        Role? existingRole = repository.GetById(TestIdForUpdate);
        if (existingRole == null)
        {
            Assert.Fail($"Role with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingRole.Name = $"Updated{existingRole.Name}";
        repository.Update(existingRole);
        Role? updatedPublisher = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPublisher, Is.Not.Null);
        Assert.That(updatedPublisher!.Name, Is.EqualTo(existingRole.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateRoleInDatabase()
    {
        IRoleRepository repository = _unitOfWork.RoleRepository;
        Role? existingRole = repository.GetById(TestIdForUpdate);
        existingRole!.Name = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingRole));
    }

    [Test]
    public void Delete_ShouldRemoveRoleFromDatabase()
    {
        IRoleRepository repository = _unitOfWork.RoleRepository;

        Role? existingRole = repository.GetById(TestIdForDelete);
        if (existingRole == null)
        {
            Assert.Fail($"Role with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        Role? deletedRole = repository.GetById(TestIdForDelete);

        Assert.That(deletedRole, Is.Null);
    }
}
