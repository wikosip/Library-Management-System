create procedure usp_InsertCity
	@CountryId int,
	@Name nvarchar(50),
	@IsoName varchar(5),
	@CityId int output
as
begin
	set nocount on;

	if LEN(@Name) < 3
	begin
	raiserror('City name must be at least 3 characters long.', 16, 1, @CityId);
	return 1;
	end

	if LEN(@IsoName) < 2
	begin
	raiserror('Iso name must be at least 2 characters long.', 16, 1, @CityId);
	return 2;
	end

	if not exists (select 1 from Countries where CountryId = @CountryId and IsDeleted = 0)
	begin
	raiserror('@CountryId does not exist.', 16, 1, @CountryId);
	return 3;
	end

	if exists (select 1 from Cities where IsoName = @IsoName and IsDeleted = 0)
	begin
	raiserror('IsoName already exists in cities', 16, 1, @IsoName);
	return 4;
	end

	insert into Cities (Name, CountryId, IsoName)
	values (@Name, @CountryId, @IsoName);
	set @CityId = scope_identity();

	return 0;
end