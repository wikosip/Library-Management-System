create procedure usp_UpdateEmployee
    @EmployeeId int,
    @CityId int,
    @PositionId int,
    @FirstName nvarchar(30),
    @LastName nvarchar(30),
    @PersonalNumber char(11),
    @Address nvarchar(50),
    @PhoneNumber varchar(20),
    @Email varchar(50)
as
begin
    set nocount on;
	if @FirstName is null or len(@FirstName) < 3
	begin
		raiserror('FirstName must be at least 3 symbols.', 16, 1);
		return 1;
	end

	if @LastName is null or len(@LastName) < 3
	begin
		raiserror('LastName must be at least 3 symbols.', 16, 1);
		return 2;
	end

	if @PersonalNumber is null or len(@PersonalNumber) != 11
	begin
		raiserror('PersonalNumber must be exactly 11 characters.', 16, 1);
		return 3;
	end

    if not exists (select 1 from Employees where EmployeeId = @EmployeeId)
    begin
        raiserror('EmployeeId %d does not exist.', 16, 1, @EmployeeId);
        return 4;
    end

    if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
    begin
        raiserror('CityId %d does not exist or is deleted.', 16, 1, @CityId);
        return 5;
    end

    if not exists (select 1 from Positions where PositionId = @PositionId and IsDeleted = 0)
    begin
        raiserror('PositionId %d does not exist or is deleted.', 16, 1, @PositionId);
        return 6;
    end

    if exists (
        select 1
        from Employees
        where PersonalNumber = @PersonalNumber
          and EmployeeId != @EmployeeId
    )
    begin
        raiserror('Another employee with the same PersonalNumber already exists.', 16, 1);
        return 1;
    end

    update Employees
    set CityId = @CityId,
        PositionId = @PositionId,
        FirstName = @FirstName,
        LastName = @LastName,
        PersonalNumber = @PersonalNumber,
        Address = @Address,
        PhoneNumber = @PhoneNumber,
        Email = @Email,
        UpdateDate = getdate()
    where EmployeeId = @EmployeeId
      and (
            CityId != @CityId or
            PositionId != @PositionId or
            FirstName != @FirstName or
            LastName != @LastName or
            PersonalNumber != @PersonalNumber or
            Address != @Address or
            PhoneNumber != @PhoneNumber or
            Email != @Email
          );

    return 0;
end