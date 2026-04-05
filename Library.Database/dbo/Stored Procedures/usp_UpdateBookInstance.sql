create procedure usp_UpdateBookInstance
    @BookInstanceId int,
    @BookId int,
    @ConditionId int
as
begin
    set nocount on;

    if not exists (select 1 from BookInstances where BookInstanceId = @BookInstanceId and IsDeleted = 0)
    begin
        raiserror('BookInstanceId %d does not exist.', 16, 1, @BookInstanceId);
        return 1;
    end

    if not exists (select 1 from Books where BookId = @BookId and IsDeleted = 0)
    begin
        raiserror('BookId %d does not exist.', 16, 1, @BookId);
        return 1;
    end

    if exists (select 1 from Books where BookId = @BookId and IsDeleted = 1)
    begin
        raiserror('BookId %d is deleted.', 16, 1, @BookId);
        return 1;
    end

    if not exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 0)
    begin
        raiserror('ConditionId %d does not exist.', 16, 1, @ConditionId);
        return 1;
    end

    if exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 1)
    begin
        raiserror('ConditionId %d is deleted.', 16, 1, @ConditionId);
        return 1;
    end

    update BookInstances
    set BookId = @BookId,
        ConditionId = @ConditionId,
        UpdateDate = getdate()
    where BookInstanceId = @BookInstanceId
      and (
            BookId != @BookId or
            ConditionId != @ConditionId
          );

    return 0;
end