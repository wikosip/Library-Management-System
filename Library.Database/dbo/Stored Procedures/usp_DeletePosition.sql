create procedure usp_DeletePosition
    @PositionId int
as
begin
    set nocount on;

    if not exists (select 1 from Positions where PositionId = @PositionId)
    begin
        raiserror('PositionId %d does not exist in the database.', 16, 1, @PositionId);
        return 1;
    end

    if exists (select 1 from Positions where PositionId = @PositionId and IsDeleted = 1)
    begin
        raiserror('PositionId %d is already deleted.', 16, 1, @PositionId);
        return 1;
    end

    update Positions
    set IsDeleted = 1,
        UpdateDate = getdate()
    where PositionId = @PositionId;

    return 0;
end