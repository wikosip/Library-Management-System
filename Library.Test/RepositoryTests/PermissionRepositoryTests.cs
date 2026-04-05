using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class PermissionRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewPermissionToDatabase()
    {
        IPermissionRepository repository = _unitOfWork.PermissionRepository;
        Permission newPermission = new()
        {
            Name = "TestPermission"
        };

        int id = repository.Insert(newPermission);
        Permission? insertedPermission = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedPermission, Is.Not.Null);
        Assert.That(insertedPermission!.Name, Is.EqualTo(newPermission.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewPermissionToDatabase()
    {
        IPermissionRepository repository = _unitOfWork.PermissionRepository;
        Permission newPermission = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newPermission));
    }

    [Test]
    public void Update_ShouldUpdatePermissionInDatabase()
    {
        IPermissionRepository repository = _unitOfWork.PermissionRepository;

        Permission? existingPermission = repository.GetById(TestIdForUpdate);
        if (existingPermission == null)
        {
            Assert.Fail($"Permission with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingPermission.Name = $"Updated{existingPermission.Name}";
        repository.Update(existingPermission);
        Permission? updatedPermission = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPermission, Is.Not.Null);
        Assert.That(updatedPermission!.Name, Is.EqualTo(existingPermission.Name));
    }

    [Test]
    public void Update_ShouldNotUpdatePermissionInDatabase()
    {
        IPermissionRepository repository = _unitOfWork.PermissionRepository;
        Permission? existingPermission = repository.GetById(TestIdForUpdate);
        existingPermission!.Name = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingPermission));
    }

    [Test]
    public void Delete_ShouldRemovePermissionFromDatabase()
    {
        IPermissionRepository repository = _unitOfWork.PermissionRepository;

        Permission? existingPermission = repository.GetById(TestIdForDelete);
        if (existingPermission == null)
        {
            Assert.Fail($"Permission with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        Permission? deletedPermission = repository.GetById(TestIdForDelete);

        Assert.That(deletedPermission, Is.Null);
    }
}
