create procedure usp_InsertBookInstance
    @BookId int,
    @ConditionId int,
    @BookInstanceId int output
as
begin
    set nocount on;

    if not exists (select 1 from Books where BookId = @BookId and IsDeleted = 0)
    begin
        raiserror('BookId %d does not exist.', 16, 1, @BookId);
        return 1;
    end

    if exists (select 1 from Books where BookId = @BookId and IsDeleted = 1)
    begin
        raiserror('BookId %d is deleted.', 16, 1, @BookId);
        return 2;
    end

    if not exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 0)
    begin
        raiserror('ConditionId %d does not exist.', 16, 1, @ConditionId);
        return 3;
    end

    if exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 1)
    begin
        raiserror('ConditionId %d is deleted.', 16, 1, @ConditionId);
        return 4;
    end

    insert into BookInstances (BookId, ConditionId)
    values (@BookId, @ConditionId);

    set @BookInstanceId = scope_identity();
    return 0;
end