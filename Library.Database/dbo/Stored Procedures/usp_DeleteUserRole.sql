create procedure usp_DeleteUserRole
@UserId int,
@RoleId int
as
begin
	if not exists (select 1 from UserAccounts where EmployeeId = @UserId and IsDeleted = 0)
	begin
	raiserror ('UserId does not exist', 16, 1)
	return 1;
	end

	if not exists (select 1 from Roles where RoleId = @RoleId and IsDeleted = 0)
	begin
	raiserror ('RoleId does not exists', 16, 2)
	return 2;
	end

	delete from UserRoles
	where UserId = @UserId and
		  RoleId = @RoleId
	
	return 0;

end