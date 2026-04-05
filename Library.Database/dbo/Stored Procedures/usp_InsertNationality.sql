--gio
create procedure usp_InsertNationality
    @Name nvarchar(20),
    @NationalityId int output
as
begin
    set nocount on;

	if len(@Name) < 3
	begin
	raiserror ('Nationality name must be min 2 symbols', 16, 2);
	return 1;
	end

	if exists (select 1 from Nationalities where Name = @Name and IsDeleted = 0)
	begin
	raiserror ('Nationality name already exist', 16, 2)
	return 2;
	end

    insert into Nationalities (Name)
    values (@Name);

    set @NationalityId = scope_identity();
    return 0;
end