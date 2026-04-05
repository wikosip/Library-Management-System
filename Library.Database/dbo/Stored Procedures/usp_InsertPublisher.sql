create procedure usp_InsertPublisher
    @Name nvarchar(200),
    @PublisherId int output
as
begin
    set nocount on;

	if @Name is null or len(@Name) < 3
	begin
	raiserror ('Publisher name must be min 2 symbols', 16, 2);
	return 1;
	end

    if exists (select 1 from Publishers where Name = @Name and IsDeleted = 0)
    begin
        raiserror('Publisher name already exists.', 16, 1);
        return 2;
    end

    insert into Publishers (Name)
    values (@Name);

    set @PublisherId = scope_identity();
    return 0;
end