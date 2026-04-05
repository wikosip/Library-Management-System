create procedure usp_UpdatePosition
    @PositionId int,
    @Name nvarchar(50),
    @Description nvarchar(250)
as
begin
    set nocount on;

    if not exists (select 1 from Positions where PositionId = @PositionId and IsDeleted = 0)
    begin
        raiserror('PositionId %d does not exist.', 16, 1, @PositionId);
        return 1;
    end

    if @Name is null or len(@Name) < 3
    begin
        raiserror('Position name must be at least 3 symbols.', 16, 1);
        return 2;
    end

    if @Description is null or len(@Description) < 3
    begin
        raiserror('Position description must be at least 3 symbols.', 16, 1);
        return 3;
    end

    if exists (
        select 1
        from Positions
        where Name = @Name
          and PositionId != @PositionId
          and IsDeleted = 0
    )
    begin
        raiserror('Another Position with name "%s" already exists.', 16, 1, @Name);
        return 4;
    end

    update Positions
    set Name = @Name,
        Description = @Description,
        UpdateDate = getdate()
    where PositionId = @PositionId and 
		  (Name != @Name or
           Description != @Description 
          );

    return 0;
end