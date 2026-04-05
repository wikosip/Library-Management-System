using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

internal class BookLoanRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewBookLoanWithValidData()
    {
        IBookLoanRepository repository = _unitOfWork.BookLoanRepository;
        BookLoan newbookloan = new()
        {
            BookInstanceId = 1,
            ConditionId = 1,
            CustomerId = 1,
            UnitPrice = 99,
            Discount = 1.90f,
            EndDate = DateTime.Parse("2026-12-12")
        };

        var id = repository.Insert(newbookloan);
        var insertedBookLoan = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedBookLoan, Is.Not.Null);
        Assert.That(insertedBookLoan!.BookInstanceId, Is.EqualTo(newbookloan.BookInstanceId));
        Assert.That(insertedBookLoan.ConditionId, Is.EqualTo(newbookloan.ConditionId));
        Assert.That(insertedBookLoan!.UnitPrice, Is.EqualTo(newbookloan.UnitPrice));
        Assert.That(insertedBookLoan.Discount, Is.EqualTo(newbookloan.Discount));
        Assert.That(insertedBookLoan!.EndDate, Is.EqualTo(newbookloan.EndDate));
    }

    [Test]
    public void Insert_ShouldNotAddNewBookLoanWithInvalidData()
    {
        IBookLoanRepository repository = _unitOfWork.BookLoanRepository;
        BookLoan newBookLoan = new()
        {
            BookInstanceId = 1,
            ConditionId = -1,
            CustomerId = 1,
            UnitPrice = 99,
            Discount = 1.90f,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            RealEndDate = DateTime.Now,
            FineAmount = -10,
            IsReturned = false
        };

        Assert.Throws<SqlException>(() => repository.Insert(newBookLoan));
    }

    [Test]
    public void Update_ShouldUpdateNewBookLoanWithValidData()
    {
        IBookLoanRepository repository = _unitOfWork.BookLoanRepository;

        var existingBookLoan = repository.GetById(TestIdForUpdate);
        if (existingBookLoan == null)
        {
            Assert.Fail($"BookLoan with price like {TestIdForUpdate} does not exist in the database.");
            return;
        }

        existingBookLoan.UnitPrice = 100;
        existingBookLoan.Discount = 10;
        repository.Update(existingBookLoan);
        var updatedBookLoan = repository.GetById(TestIdForUpdate);

        Assert.That(updatedBookLoan, Is.Not.Null);
        Assert.That(updatedBookLoan!.UnitPrice, Is.EqualTo(existingBookLoan.UnitPrice));
    }

    [Test]
    public void Update_ShouldNotUpdateNewBookWithInvalidData()
    {
        IBookLoanRepository repository = _unitOfWork.BookLoanRepository;
        var newBookLoan = repository.GetById(TestIdForUpdate);
        newBookLoan!.BookLoanId = -1;

        Assert.Throws<SqlException>(() => repository.Update(newBookLoan));
    }
}
