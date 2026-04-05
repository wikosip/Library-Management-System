--gagi
create procedure usp_InsertPosition
    @Name nvarchar(50),
    @Description nvarchar(250),
    @PositionId int output
as
begin
    set nocount on;

    if @Name is null or len(@Name) < 3
    begin
        raiserror('Position name must be at least 3 symbols.', 16, 1);
        return 1;
    end

    if @Description is null or len(@Description) < 3
    begin
        raiserror('Position description must be at least 3 symbols.', 16, 1);
        return 2;
    end

    if exists (select 1 from Positions where Name = @Name and IsDeleted = 0)
    begin
        raiserror('Position with name "%s" already exists.', 16, 1, @Name);
        return 3;
    end

    insert into Positions (Name, Description)
    values (@Name, @Description);

    set @PositionId = scope_identity();
    return 0;
end