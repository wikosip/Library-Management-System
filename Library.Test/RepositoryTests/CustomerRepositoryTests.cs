using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class CustomerRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewCustomerToDatabase()
    {
        ICustomerRepository repository = _unitOfWork.CustomerRepository;

        Customer newCustomer = new()
        {
            CityId = 1,
            PersonalNumber = "12345678901",
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main Street",
            Email = "john.doe@test.com",
            PhoneNumber = "555123456"
        };

        int id = repository.Insert(newCustomer);
        Customer? insertedCustomer = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedCustomer, Is.Not.Null);
        Assert.That(insertedCustomer!.CustomerId, Is.EqualTo(id));
        Assert.That(insertedCustomer.CityId, Is.EqualTo(newCustomer.CityId));
        Assert.That(insertedCustomer.PersonalNumber, Is.EqualTo(newCustomer.PersonalNumber));
        Assert.That(insertedCustomer.FirstName, Is.EqualTo(newCustomer.FirstName));
        Assert.That(insertedCustomer.LastName, Is.EqualTo(newCustomer.LastName));
        Assert.That(insertedCustomer.BirthDate, Is.EqualTo(newCustomer.BirthDate));
        Assert.That(insertedCustomer.Address, Is.EqualTo(newCustomer.Address));
        Assert.That(insertedCustomer.Email, Is.EqualTo(newCustomer.Email));
        Assert.That(insertedCustomer.PhoneNumber, Is.EqualTo(newCustomer.PhoneNumber));
        Assert.That(insertedCustomer.IsDeleted, Is.False);
    }

    [Test]
    public void Insert_ShouldNotAddNewCustomerToDatabase()
    {
        ICustomerRepository repository = _unitOfWork.CustomerRepository;

        Customer newCustomer = new()
        {
            CityId = 1,
            PersonalNumber = "12345",
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main Street",
            Email = "john.doe@test.com",
            PhoneNumber = "555123456"
        };

        Assert.Throws<SqlException>(() => repository.Insert(newCustomer));
    }

    [Test]
    public void Update_ShouldUpdateCustomerInDatabase()
    {
        ICustomerRepository repository = _unitOfWork.CustomerRepository;

        Customer? existingCustomer = repository.GetById(TestIdForUpdate);
        if (existingCustomer == null)
        {
            Assert.Fail($"Customer with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingCustomer.FirstName = $"Updated{existingCustomer.FirstName}";
        existingCustomer.LastName = $"Updated{existingCustomer.LastName}";
        existingCustomer.Address = "Updated Address";
        existingCustomer.BirthDate = DateTime.Now;

        repository.Update(existingCustomer);

        Customer? updatedCustomer = repository.GetById(TestIdForUpdate);

        Assert.That(updatedCustomer, Is.Not.Null);
        Assert.That(updatedCustomer!.FirstName, Is.EqualTo(existingCustomer.FirstName));
        Assert.That(updatedCustomer.LastName, Is.EqualTo(existingCustomer.LastName));
        Assert.That(updatedCustomer.Address, Is.EqualTo(existingCustomer.Address));
    }

    [Test]
    public void Update_ShouldNotUpdateNewCustomerWithInvalidData()
    {
        ICustomerRepository repository = _unitOfWork.CustomerRepository;
        var newCustomer = repository.GetById(TestIdForUpdate);
        newCustomer!.FirstName = null!;
        newCustomer.BirthDate = new DateTime(2025, 1, 1);

        Assert.Throws<SqlException>(() => repository.Update(newCustomer));
    }

    [Test]
    public void Delete_ShouldRemoveCustomerFromDatabase()
    {
        ICustomerRepository repository = _unitOfWork.CustomerRepository;

        Customer? existingCustomer = repository.GetById(TestIdForDelete);
        if (existingCustomer == null)
        {
            Assert.Fail($"Customer with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        Customer? deletedCustomer = repository.GetById(TestIdForDelete);

        Assert.That(deletedCustomer, Is.Null);
    }
}
