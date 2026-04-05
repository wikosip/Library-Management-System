using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class PaymentRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewPaymentToDatabase()
    {
        IPaymentRepository repository = _unitOfWork.PaymentRepository;
        Payment newPayment = new()
        {
            Amount = 14,
            CustomerId = 1,
            EmployeeId = 1
        };

        int id = repository.Insert(newPayment);
        Payment? insertedPayment = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedPayment, Is.Not.Null);
        Assert.That(insertedPayment!.Amount, Is.EqualTo(newPayment.Amount));

    }
    [Test]
    public void Insert_ShouldNotAddNewPaymentToDatabase()
    {
        IPaymentRepository repository = _unitOfWork.PaymentRepository;
        Payment newPayment = new()
        {
            Amount = 0
        };

        Assert.Throws<SqlException>(() => repository.Insert(newPayment));
    }
    [Test]
    public void Update_ShouldUpdatePaymentInDatabase()
    {
        IPaymentRepository repository = _unitOfWork.PaymentRepository;

        Payment? existingPayment = repository.GetById(TestIdForUpdate);
        if (existingPayment == null)
        {
            Assert.Fail($"Payment with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingPayment.Amount = 50;
        repository.Update(existingPayment);

        Payment? updatedPayment = repository.GetById(TestIdForUpdate);

        Assert.That(updatedPayment, Is.Not.Null);
        Assert.That(updatedPayment!.Amount, Is.EqualTo(existingPayment.Amount));
    }

    [Test]
    public void Update_ShouldNotUpdatePaymentInDatabase()
    {
        IPaymentRepository repository = _unitOfWork.PaymentRepository;
        Payment? existingPayment = repository.GetById(TestIdForUpdate);
        existingPayment!.Amount = -10;
        Assert.Throws<SqlException>(() => repository.Update(existingPayment));
    }
}
