create procedure usp_UpdateRole
@RoleId int,
@Name nvarchar(50)
as
begin
	if @Name is null or len(@Name) < 3
	begin
	raiserror ('Role name must be at least 3 symbols', 16, 3);
	return 1;
	end

	if not exists (select 1 from Roles where RoleId = @RoleId and IsDeleted = 0)
	begin
	raiserror ('RoleId does not exist', 16, 3);
	return 2;
	end

	if exists (select 1 from Roles where Name = @Name and RoleId != @RoleId and IsDeleted = 0)
	begin
	raiserror ('Role name already exist', 16, 2);
	return 3;
	end

	update Roles
	set Name = @Name,
		UpdateDate = getdate()
		where RoleId = @RoleId;

	return 0;
end