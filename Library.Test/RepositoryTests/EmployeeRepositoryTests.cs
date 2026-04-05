using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.RepositoryTests;

internal class EmployeeRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewEmployeeToDatabase()
    {
        IEmployeeRepository repository = _unitOfWork.EmployeeRepository;
        Employee newEmployee = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St",
            Email = "123",
            PhoneNumber = "123",
            PersonalNumber = "12345678910",
            PositionId = 1,
            CityId = 1
        };

        int id = repository.Insert(newEmployee);
        Employee? insertedEmployee = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedEmployee, Is.Not.Null);
        Assert.That(insertedEmployee!.FirstName, Is.EqualTo(newEmployee.FirstName));
        Assert.That(insertedEmployee.LastName, Is.EqualTo(newEmployee.LastName));
        Assert.That(insertedEmployee.Address, Is.EqualTo(newEmployee.Address));
        Assert.That(insertedEmployee.Email, Is.EqualTo(newEmployee.Email));
        Assert.That(insertedEmployee.PhoneNumber, Is.EqualTo(newEmployee.PhoneNumber));
        Assert.That(insertedEmployee.PersonalNumber, Is.EqualTo(newEmployee.PersonalNumber));
        Assert.That(insertedEmployee.PositionId, Is.EqualTo(newEmployee.PositionId));
        Assert.That(insertedEmployee.CityId, Is.EqualTo(newEmployee.CityId));
    }

    [Test]
    public void Insert_ShouldNotAddNewEmployeeToDatabase()
    {
        IEmployeeRepository repository = _unitOfWork.EmployeeRepository;
        Employee newEmployee = new()
        {
            FirstName = null!,
            LastName = null!,
            Address = null!,
            PersonalNumber = null!,
            Email = null!,
            PositionId = 0
        }; 

        Assert.Throws<SqlException>(() => repository.Insert(newEmployee));
    }

    [Test]
    public void Update_ShouldUpdatePublisherInDatabase()
    {
        IEmployeeRepository repository = _unitOfWork.EmployeeRepository;

        Employee? existingEmployee = repository.GetById(TestIdForUpdate);
        if (existingEmployee == null)
        {
            Assert.Fail($"Employee with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingEmployee.FirstName = $"Updated{existingEmployee.FirstName}";
        existingEmployee.LastName = $"Updated{existingEmployee.LastName}";
        existingEmployee.Address = $"Updated{existingEmployee.Address}";
        existingEmployee.Email = $"Updated{existingEmployee.Email}";
        existingEmployee.PhoneNumber = $"Updated{existingEmployee.PhoneNumber}";

        repository.Update(existingEmployee);
        Employee? updatedEmployee = repository.GetById(TestIdForUpdate);

        Assert.That(updatedEmployee, Is.Not.Null);
        Assert.That(updatedEmployee!.FirstName, Is.EqualTo(existingEmployee.FirstName));
        Assert.That(updatedEmployee.LastName, Is.EqualTo(existingEmployee.LastName));
        Assert.That(updatedEmployee.Address, Is.EqualTo(existingEmployee.Address));
        Assert.That(updatedEmployee.Email, Is.EqualTo(existingEmployee.Email));
        Assert.That(updatedEmployee.PhoneNumber, Is.EqualTo(existingEmployee.PhoneNumber));
    }

    [Test]
    public void Update_ShouldNotUpdateEmployeeInDatabase()
    {
        IEmployeeRepository repository = _unitOfWork.EmployeeRepository;
        Employee? existingEmployee = repository.GetById(TestIdForUpdate);
        existingEmployee!.FirstName = null!;
        Assert.Throws<SqlException>(() => repository.Update(existingEmployee));
    }

    [Test]
    public void Delete_ShouldRemoveEmployeeFromDatabase()
    {
        IEmployeeRepository repository = _unitOfWork.EmployeeRepository;

        Employee? existingEmployee = repository.GetById(TestIdForDelete);
        if (existingEmployee == null)
        {
            Assert.Fail($"Employee with ID {TestIdForDelete} does not exist in the database.");
            return;
        }

        repository.Delete(TestIdForDelete);
        Employee? deletedEmployee = repository.GetById(TestIdForDelete);

        Assert.That(deletedEmployee, Is.Null);
    }
}
