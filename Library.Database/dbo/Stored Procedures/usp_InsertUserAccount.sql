create procedure usp_InsertUserAccount
@EmployeeId int,
@Username varchar(50),
@Password varbinary(255)
as
begin
	set nocount on;

	if len(@Username) < 3
	begin
	raiserror ('Username must be min 2 symbols', 16, 2);
	return 1;
	end

	if exists (select 1 from UserAccounts where Username = @Username and IsActive = 1 and IsDeleted = 0)
	begin
	raiserror ('Username already exist', 16, 3);
	return 2;
	end

	if len(@Password) < 5
	begin
	raiserror ('Password must be min 5 symbols', 16,3);
	return 3;
	end

	if not exists (select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
	begin
	raiserror ('Employee does not exist', 16, 3)
	return 4;
	end

	insert into UserAccounts(EmployeeId, Username, Password)
	values (@EmployeeId ,@Username, @Password)

	return 0;
end