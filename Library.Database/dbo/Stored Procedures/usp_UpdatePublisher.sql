create procedure usp_UpdatePublisher
    @PublisherId int,
    @Name nvarchar(200)
as
begin
    set nocount on;

    if @Name is null or len(@Name) < 3
    begin
        raiserror('Publisher name must be at least 3 symbols', 16, 2)
        return 1;
    end

	if exists (select 1 from Publishers where Name = @Name and PublisherId != @PublisherId and IsDeleted = 0)
    begin
        raiserror('Publisher name already exists.', 16, 1);
        return 2;
    end

    if not exists (select 1 from Publishers where PublisherId = @PublisherId and IsDeleted = 0)
    begin
        raiserror('PublisherId does not exist.', 16, 1);
        return 3;
    end

    update Publishers
    set Name = @Name,
        UpdateDate = getdate()
    where PublisherId = @PublisherId and Name != @Name;

    return 0;
end