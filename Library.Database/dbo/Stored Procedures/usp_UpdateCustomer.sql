create procedure usp_UpdateCustomer
@CustomerId int,
@CityId int,
@PersonalNumber char(11),
@FirstName nvarchar(20),
@LastName nvarchar(30),
@BirthDate date = null,
@Address nvarchar(50) = null,
@Email varchar(50) = null,
@PhoneNumber varchar(20) = null
as
begin
	set nocount on;

	if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
	raiserror('customer does not exist.', 16, 1, @CustomerId);
	return 1;
	end

	if (@Address is null and @Email is null and @PhoneNumber is null)
	begin
	raiserror('field has to be existed.', 15, 1);
	return 2;
	end
	
	if @FirstName is null or len(@FirstName) < 3
	begin
	raiserror ('firstname must be min 3 symbols', 16, 3)
	return 3;
	end

	if @LastName is null or len(@LastName) < 3
	begin
	raiserror ('lastname must be min 3 symbols', 16, 3)
	return 4;
	end

	if @BirthDate is not null and (year(@BirthDate)) < 1900 or (@BirthDate) > getdate()
	begin
	raiserror ('invalid birthdate', 16, 2)
	return 5;
	end

	if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
	begin
	raiserror ('cannot find the city', 16, 1)
	return 6;
	end

	if exists (select 1 from Customers where PersonalNumber = @PersonalNumber and CustomerId != @CustomerId and IsDeleted = 0)
	begin
	raiserror ('personal number already exists', 16, 2)
	return 7;
	end

	update Customers
	set CityId = @CityId,
	FirstName = @FirstName,
	LastName = @LastName,
	BirthDate = @BirthDate,
	Address = @Address,
	Email = @Email,
	PhoneNumber = @PhoneNumber,
	UpdateDate = getdate()
	where CustomerId = @CustomerId  and (
			CityId != @CityId or
			PersonalNumber != @PersonalNumber or
			FirstName != @FirstName or
			LastName != @LastName or
			BirthDate != @BirthDate or
			Address!=  @Address or
            Email!= @Email or
            PhoneNumber != @PhoneNumber or
			(Address is null and @Address is not null) or
			(Address is not null and @Address is null) or
			(BirthDate is null and @BirthDate is not null) or
			(BirthDate is not null and @BirthDate is null) or 
			(Email is null and @Email is not null) or
			(Email is not null and @Email is null) or
			(PhoneNumber is null and @PhoneNumber is not null) or
			(PhoneNumber is not null and @PhoneNumber is null)
		  );
	return 0;
end