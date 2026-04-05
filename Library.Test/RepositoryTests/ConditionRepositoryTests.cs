using Library.DTO;
using Library.Repository;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Library.Test.RepositoryTests;

[TestFixture]
internal class ConditionRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewConditionToDatabase()
    {
        IConditionRepository repository = _unitOfWork.ConditionRepository;
        Condition newCondition = new()
        {
            Name = "Test1",
            Value = 2
        };

        int id = repository.Insert(newCondition);
        Condition? insertedCondition = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedCondition, Is.Not.Null);
        Assert.That(insertedCondition!.Name, Is.EqualTo(newCondition.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewConditionToDatabase()
    {
        IConditionRepository repository = _unitOfWork.ConditionRepository;
        Condition newCondition = new()
        {
            Name = null!,
            Value = 2
        };

        Assert.Throws<SqlException>(() => repository.Insert(newCondition));
    }

    [Test]
    public void Update_ShouldUpdateNewConditionToDatabase()
    {
        IConditionRepository repository = _unitOfWork.ConditionRepository;

        Condition? existingCondition = repository.GetById(TestIdForUpdate);
        if (existingCondition == null)
        {
            Assert.Fail("Condition with ID 1 does not exist in the database.");
            return;
        }
        existingCondition.Name = $"Updated{existingCondition.Name}";
        repository.Update(existingCondition);
        Condition? updatedCondition = repository.GetById(TestIdForUpdate);

        Assert.That(updatedCondition, Is.Not.Null);
        Assert.That(updatedCondition!.Name, Is.EqualTo(existingCondition.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateNewConditionWithInvalidData()
    {
        IConditionRepository repository = _unitOfWork.ConditionRepository;
        var newCondition = repository.GetById(TestIdForUpdate);
        newCondition!.Name = null!;

        Assert.Throws<SqlException>(() => repository.Update(newCondition));
    }

    [Test]
    public void Delete_ShouldRemoveConditionFromDatabase()
    {
        IConditionRepository repository = _unitOfWork.ConditionRepository;
        Condition? existingCondition = repository.GetById(TestIdForDelete);
        if (existingCondition == null)
        {
            Assert.Fail($"Condition with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        Condition? deletedCondition = repository.GetById(TestIdForDelete);

        Assert.That(deletedCondition, Is.Null);
    }
}