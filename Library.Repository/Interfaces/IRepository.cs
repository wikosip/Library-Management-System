namespace Library.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    public T? GetById(object id);
    public int Insert(T entity);
    public int Update(T entity); // Todo: Update should return void.
    public int Delete(object id); // Todo: Delete should return void.
}