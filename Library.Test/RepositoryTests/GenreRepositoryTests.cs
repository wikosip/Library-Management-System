using Library.DTO;
using Library.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.RepositoryTests;

internal class GenreRepositoryTests : RepositoryBaseTest
{
    [Test]
    public void Insert_ShouldAddNewGenreToDatabase()
    {
        IGenreRepository repository = _unitOfWork.GenreRepository!;
        Genre newGenre = new()
        {
            Name = "TestGenre"
        };

        int id = repository.Insert(newGenre);
        Genre? insertedGenre = repository.GetById(id);

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedGenre, Is.Not.Null);
        Assert.That(insertedGenre!.Name, Is.EqualTo(newGenre.Name));
    }

    [Test]
    public void Insert_ShouldNotAddNewGenreToDatabase()
    {
        IGenreRepository repository = _unitOfWork.GenreRepository!;
        Genre newGenre = new()
        {
            Name = null!
        };

        Assert.Throws<SqlException>(() => repository.Insert(newGenre));
    }

    [Test]
    public void Update_ShouldUpdateNewGebreToDatabase()
    {
        IGenreRepository repository = _unitOfWork.GenreRepository!;

        Genre? existingGenre = repository.GetById(TestIdForUpdate);
        if (existingGenre == null)
        {
            Assert.Fail($"Genre with ID {TestIdForUpdate} does not exist in the database.");
            return;
        }
        existingGenre.Name = $"Updated{existingGenre.Name}";
        Genre? updatedGenre = repository.GetById(TestIdForUpdate);

        Assert.That(updatedGenre, Is.Not.Null);
        Assert.That(updatedGenre!.Name, Is.EqualTo(updatedGenre.Name));
    }

    [Test]
    public void Update_ShouldNotUpdateNewGenreWithInvalidData()
    {
        IGenreRepository repository = _unitOfWork.GenreRepository!;
        var existingGenre = repository.GetById(TestIdForUpdate);
        existingGenre!.Name = null!;

        Assert.Throws<SqlException>(() => repository.Update(existingGenre));
    }

    [Test]
    public void Delete_ShouldRemoveGenreFromDatabase()
    {
        IGenreRepository repository = _unitOfWork.GenreRepository!;
        Genre? existingGenre = repository.GetById(TestIdForDelete);
        if (existingGenre == null)
        {
            Assert.Fail($"Genre with ID {TestIdForDelete} does not exist in the database.");
            return;
        }
        repository.Delete(TestIdForDelete);
        Genre? deletedGenre = repository.GetById(TestIdForDelete);

        Assert.That(deletedGenre, Is.Null);
    }
}
