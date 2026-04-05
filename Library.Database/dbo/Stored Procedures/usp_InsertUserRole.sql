create procedure usp_InsertUserRole
@UserId int,
@RoleId int
as
begin
	set nocount on;

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

	if exists (select 1 from UserRoles where UserId = @UserId and RoleId = @RoleId)
    begin
        raiserror('This role is already assigned to user', 16, 1);
        return 3;
    end

	insert into UserRoles(UserId, RoleId)
	values (@UserId, @RoleId)

	return 0;
end