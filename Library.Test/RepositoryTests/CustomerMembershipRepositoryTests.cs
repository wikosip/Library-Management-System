using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

[TestFixture]
internal class CustomerMembershipRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewCustomerMembershipWithValidData()
    {
        ICustomerMembershipRepository repository = _unitOfWork.CustomerMembershipRepository;
        CustomerMembership newCostumerMembership = new()
        {
            CustomerId = 1,
            MembershipTypeId = 1,
            EndDate = new DateTime(2020, 11, 1),
        };

        var id = repository.Insert(newCostumerMembership);
        var insertedCustomerMembership = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedCustomerMembership, Is.Not.Null);
        Assert.That(insertedCustomerMembership!.CustomerId, Is.EqualTo(newCostumerMembership.CustomerId));
        Assert.That(insertedCustomerMembership.MembershipTypeId, Is.EqualTo(newCostumerMembership.MembershipTypeId));
        Assert.That(insertedCustomerMembership.EndDate, Is.EqualTo(newCostumerMembership.EndDate));
    }

    [Test]
    public void Insert_ShouldNotAddCustomerMembershipWithInvalidData()
    {
        ICustomerMembershipRepository repository = _unitOfWork.CustomerMembershipRepository;
        CustomerMembership newCostumerMembership = new()
        {
            CustomerId = 999,
            MembershipTypeId = 1,
            EndDate = new DateTime(2027, 11, 1)
        };
        Assert.Throws<SqlException>(() => repository.Insert(newCostumerMembership));
    }
    [Test]
    public void Update_ShouldUpdateCustomerMembershipWithValidData()
    {
        ICustomerMembershipRepository repository = _unitOfWork.CustomerMembershipRepository;

        var existingCostumerMembership = repository.GetById(TestIdForUpdate);
        if (existingCostumerMembership == null)
        {
            Assert.Fail($"CustomerMembership with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        existingCostumerMembership.CustomerId = 1;
        repository.Update(existingCostumerMembership);
        var updatedCostumerMembership = repository.GetById(TestIdForUpdate);

        Assert.That(updatedCostumerMembership, Is.Not.Null);
        Assert.That(updatedCostumerMembership!.CustomerId, Is.EqualTo(1));
    }
    [Test]
    public void Update_ShouldNotUpdateNewCountryWithInvalidData()
    {
        ICustomerMembershipRepository repository = _unitOfWork.CustomerMembershipRepository;
        var existingCostumerMembership = repository.GetById(TestIdForUpdate)!;
        existingCostumerMembership.CustomerId = 99999;

        Assert.Throws<SqlException>(() => repository.Update(existingCostumerMembership));
    }
    [Test]
    public void Delete_ShouldRemoveCustomerMembershipFromDatabase()
    {
        ICustomerMembershipRepository repository = _unitOfWork.CustomerMembershipRepository;

        var existingCustomerMembership = repository.GetById(TestIdForDelete);
        if (existingCustomerMembership == null)
        {
            Assert.Fail($"CustomerMembership with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        var deletedCostumerMembership = repository.GetById(TestIdForDelete);

        Assert.That(deletedCostumerMembership, Is.Null);
    }
}
