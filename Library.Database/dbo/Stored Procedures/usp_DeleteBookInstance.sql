create procedure usp_DeleteBookInstance
    @BookInstanceId int
as
begin
    set nocount on;

    if not exists (select 1 from BookInstances where BookInstanceId = @BookInstanceId)
    begin
        raiserror('BookInstanceId %d does not exist in the database.', 16, 1, @BookInstanceId);
        return 1;
    end

    if exists (select 1 from BookInstances where BookInstanceId = @BookInstanceId and IsDeleted = 1)
    begin
        raiserror('BookInstanceId %d is already deleted.', 16, 1, @BookInstanceId);
        return 1;
    end

    update BookInstances
    set IsDeleted = 1,
        UpdateDate = getdate()
    where BookInstanceId = @BookInstanceId;

    return 0;
end