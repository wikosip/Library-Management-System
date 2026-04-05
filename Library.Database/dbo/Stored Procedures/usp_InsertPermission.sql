create procedure usp_InsertPermission
@Name nvarchar(50),
@PermissionId int output
as
begin
	set nocount on;

	if @Name is null or len(@Name) < 3 
	begin
	raiserror ('permission name must be min 3 symbols', 16, 1);
	return 1;
	end

	if exists (select 1 from Permissions where Name = @Name and IsDeleted = 0)
	begin
	raiserror('Permissions with name already exists.', 16, 1, @Name);
	return 1;
	end

	insert into Permissions (Name)
	values (@Name);
    set @PermissionId = scope_identity();
	return 0;
end