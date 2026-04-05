create procedure usp_DeleteUserAccount
@EmployeeId int
as
begin
	if not exists (select 1 from UserAccounts where EmployeeId = @EmployeeId and IsDeleted = 0)
	begin
	raiserror('EmployeeId does not exist', 16, 2)
	return 1;
	end

	update UserAccounts
	set IsDeleted  = 1,
		IsActive = 0,
		UpdateDate = GETDATE()
	where EmployeeId = @EmployeeId;

	return 0;
end