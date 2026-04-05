create procedure usp_UpdatePermission
@PermissionId int,
@Name nvarchar(50)
as
begin
	set nocount on;

	if @Name is null or len(@Name) < 3 
	begin
	raiserror ('permission name must be min 3 symbols', 16, 1);
	return 1;
	end

	if not exists (select 1 from Permissions where PermissionId = @PermissionId and IsDeleted = 0)
	begin
	raiserror('PermissionsId does not exist.', 16, 1, @PermissionId);
	return 1;
	end

	if exists (select 1 from Permissions where Name = @Name and PermissionId != @PermissionId and IsDeleted = 0)
	begin
	raiserror('Permissions name already exists.', 14, 1, @Name);
	return 1;
	end

	update Permissions
	set Name = @Name,
	UpdateDate = getdate()
	where PermissionId = @PermissionId
	and Name != @Name;
	return 0;
end