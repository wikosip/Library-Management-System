create procedure usp_InsertEmployee
    @CityId int,
    @PositionId int,
    @FirstName nvarchar(30),
    @LastName nvarchar(30),
    @PersonalNumber char(11),
    @Address nvarchar(50),
    @PhoneNumber varchar(20),
    @Email varchar(50),
    @EmployeeId int output
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

	if exists (select 1 from Employees where PersonalNumber = @PersonalNumber and IsDeleted = 0)
	begin
		raiserror('Employee with PersonalNumber %s already exists.', 16, 1, @PersonalNumber);
		return 4;
	end

    if not exists (select 1 from Cities where CityId = @CityId)
    begin
        raiserror('CityId %d does not exist.', 16, 1, @CityId);
        return 5;
    end

    if exists (select 1 from Cities where CityId = @CityId and IsDeleted = 1)
    begin
        raiserror('CityId %d is deleted.', 16, 1, @CityId);
        return 6;
    end

    if not exists (select 1 from Positions where PositionId = @PositionId)
    begin
        raiserror('PositionId %d does not exist.', 16, 1, @PositionId);
        return 7;
    end

    if exists (select 1 from Positions where PositionId = @PositionId and IsDeleted = 1)
    begin
        raiserror('PositionId %d is deleted.', 16, 1, @PositionId);
        return 8;
    end

    insert into Employees
        (CityId, PositionId, FirstName, LastName, PersonalNumber, Address, PhoneNumber, Email)
    values
        (@CityId, @PositionId, @FirstName, @LastName, @PersonalNumber, @Address, @PhoneNumber, @Email);

    set @EmployeeId = scope_identity();
    return 0;
end