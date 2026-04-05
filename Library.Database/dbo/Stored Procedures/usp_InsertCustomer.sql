create procedure usp_InsertCustomer
@CityId int,
@PersonalNumber char(11),
@FirstName nvarchar(20),
@Lastname nvarchar(30),
@BirthDate date null,
@Address nvarchar (50) null,
@Email varchar(50) null,
@PhoneNumber varchar(20),
@CustomerId int output
as
begin
	set nocount on;

	if (@Address is null and @Email is null and @PhoneNumber is null)
	begin
	raiserror('field has to be be existed.',15,1);
	return 1;
	end

	if len(@FirstName) < 3 or len(@Lastname) < 3
	begin
	raiserror ('firstname or lastname must be min 2 symbols', 16, 3)
	return 2;
	end

	if @BirthDate is not null and year(@BirthDate) < 1900 or (@BirthDate) > getdate()
	begin
	raiserror ('invalid birthdate', 16, 2)
	return 3;
	end

	if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
	begin
	raiserror ('cannot find the city', 16, 1)
	return 4;
	end

	if exists (select 1 from Customers where PersonalNumber = @PersonalNumber and IsDeleted = 0)
	begin
	raiserror ('personal number already exists', 16, 2)
	return 5;
	end

	insert into Customers(CityId,PersonalNumber,FirstName,LastName,BirthDate,Address,Email,PhoneNumber)
	values (@CityId,@PersonalNumber,@FirstName,@LastName,@BirthDate,@Address,@Email,@PhoneNumber);
	set @CustomerId = scope_identity();
	return 0;
end