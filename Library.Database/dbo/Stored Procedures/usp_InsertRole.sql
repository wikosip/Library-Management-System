create procedure usp_InsertRole
@Name nvarchar(50),
@RoleId int output
as
begin
	set nocount on;
	
	if @Name is null or len(@Name) < 3
	begin
	raiserror ('Role name must be at least 3 symbols', 16, 3);
	return 1;
	end

	if exists (select 1 from Roles where Name = @Name and IsDeleted = 0)
	begin
	raiserror ('Role name already exist', 16, 2);
	return 2;
	end

	insert into Roles(Name)
	values (@Name)

	set @RoleId = scope_identity();
	return 0;
end