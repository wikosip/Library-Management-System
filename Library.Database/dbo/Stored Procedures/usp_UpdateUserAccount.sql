create procedure usp_UpdateUserAccount
@EmployeeId int,
@Username varchar(50),
@Password varbinary(255),
@IsActive bit
as
begin
set nocount on;

	if len(@Username) < 3
	begin
	raiserror ('Username must be min 2 symbols', 16, 2);
	return 1;
	end

	if len(@Password) < 5
	begin
	raiserror ('Password must be min 5 symbols', 16,3);
	return 3;
	end

	if exists (select 1 from UserAccounts where Username = @Username and EmployeeId != @EmployeeId and IsActive = 1 and IsDeleted = 0)
	begin
	raiserror ('Username already exist', 16, 3);
	return 2;
	end

	if not exists (select 1 from UserAccounts where EmployeeId = @EmployeeId and IsDeleted = 0)
	begin
	raiserror ('Employee does not exist', 16, 3)
	return 4;
	end

	update UserAccounts
	set Username = @Username,
		Password = @Password,
		IsActive = @IsActive,
		UpdateDate = getdate()
		where EmployeeId = @EmployeeId and
			  (Username != @Username or
			  Password != @Password or
			  IsActive != @IsActive
		)

	return 0;
end