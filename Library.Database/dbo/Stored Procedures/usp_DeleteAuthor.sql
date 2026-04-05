create procedure usp_DeleteAuthor
    @AuthorId int
as
begin
    set nocount on;

    if not exists (select 1 from Authors where AuthorId = @AuthorId)
    begin
        raiserror('AuthorId %d does not exist in the database.', 16, 1, @AuthorId);
        return 1;
    end

    if exists (select 1 from Authors where AuthorId = @AuthorId and IsDeleted = 1)
    begin
        raiserror('AuthorId %d is already deleted.', 16, 1, @AuthorId);
        return 1;
    end

    update Authors
    set IsDeleted = 1,
        UpdateDate = getdate()
    where AuthorId = @AuthorId;

    return 0;
end