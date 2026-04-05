create procedure usp_DeleteRole
@RoleId int
as
begin
	if not exists (select 1 from Roles where RoleId = @RoleId and IsDeleted = 0)
	begin
	raiserror ('RoleId does not exist', 16, 3);
	return 1;
	end

	update Roles
	set IsDeleted = 1,
		UpdateDate = getdate()
	where RoleId = @RoleId;

	return 0;
end