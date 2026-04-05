create procedure usp_DeletePermission
@PermissionId int
as
begin
	set nocount on;

	if not exists (select 1 from Permissions where PermissionId = @PermissionId and IsDeleted = 0)
	begin
	raiserror('PermissionId does not exist.', 16, 1, @PermissionId);
	return 1;
	end

	update Permissions
	set IsDeleted = 1,
	UpdateDate = getdate()
	where PermissionId = @PermissionId;
	return 0;
end