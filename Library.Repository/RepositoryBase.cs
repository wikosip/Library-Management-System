using Dapper;
using Library.DTO.Attributes;
using Library.Extension;
using Library.Repository.Interfaces;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Library.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly DbConnection _connection;
    private readonly Func<DbTransaction?> _getTransaction;
    private readonly string _entityName;

    public RepositoryBase(DbConnection connection, Func<DbTransaction?> getTransaction)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _entityName = typeof(T).Name;
        _getTransaction = getTransaction;
    }

    public T? GetById(object id)
    {
        bool hasIsDeleted = typeof(T).GetProperty("IsDeleted") != null;
        string query = $"select * from {_entityName.ToPluralize()} " +
                       $"where {_entityName}Id = @id";
        if (hasIsDeleted)
            query += " and IsDeleted = 0";
            
        return _connection.QueryFirstOrDefault<T>(query, new { id });
    }

    public int Insert(T entity)
    {
        EnsureNotNull(entity);
        var parameters = GetInsertParameters(entity);
        parameters.Add($"@{_entityName}Id", null, DbType.Int32, ParameterDirection.Output);

        _connection.Execute(
            GetProcedureName("usp_Insert"),
            parameters,
            transaction: _getTransaction(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>($"{_entityName}Id");
    }

    public int Update(T entity)
    {
        EnsureNotNull(entity);
        return _connection.Execute(
            GetProcedureName("usp_Update"),
            GetUpdateParameters(entity),
            transaction: _getTransaction(),
            commandType: CommandType.StoredProcedure);
    }

    public int Delete(object id)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"{_entityName}Id", id);

        return _connection.Execute(
            GetProcedureName("usp_Delete"),
            parameters,
            transaction: _getTransaction(),
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters GetParameters<TAttribute>(T entity) where TAttribute : Attribute
    {
        var parameters = new DynamicParameters();
        foreach (var property in typeof(T).GetProperties())
        {
            if (Attribute.IsDefined(property, typeof(TAttribute)))
                continue;

            parameters.Add(property.Name, property.GetValue(entity));
        }

        return parameters;
    }

    private static DynamicParameters GetInsertParameters(T entity) =>
        GetParameters<IgnoreForInsert>(entity);

    private static DynamicParameters GetUpdateParameters(T entity) =>
        GetParameters<IgnoreForUpdate>(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetProcedureName(string name) =>
        $"{name}{_entityName}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureNotNull(T entity) =>
        ArgumentNullException.ThrowIfNull(entity);
}