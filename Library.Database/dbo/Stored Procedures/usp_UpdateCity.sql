create procedure usp_UpdateCity
	@CityId int,
	@Name nvarchar(50),
	@IsoName varchar(5),
	@CountryId int
as
begin
	set nocount on;

	if @Name is null or LEN(@Name) < 3
	begin
	raiserror('City name must be at least 3 characters long.', 16, 1, @CityId);
	return 1;
	end

	if @IsoName is null or LEN(@IsoName) < 2
	begin
	raiserror('Iso name must be at least 2 characters long.', 16, 1, @CityId);
	return 2;
	end

	if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
	begin
	raiserror('CityId does not exist.', 16, 1, @CityId);
	return 3;
	end

	if exists (select 1 from Cities where Name = @Name and CityId != @CityId and IsDeleted = 0)
	begin
	raiserror('City name already exists', 16, 1)
	return 4;
	end

	if not exists (select 1 from Countries where CountryId = @CountryId and IsDeleted = 0)
	begin
	raiserror('Country does not exists', 16 ,4)
	return 5;
	end

	if exists (select 1 from Cities where IsoName = @IsoName and CityId != @CityId and IsDeleted = 0)
	begin
	raiserror('IsoName already exists', 16, 1, @IsoName);
	return 6;
	end

	update Cities
	set Name = @Name,
		IsoName = @IsoName,
		CountryId = @CountryId,
		UpdateDate = getdate()
	where CityId = @CityId and 
		  (Name != @Name or 
		  IsoName != @IsoName);

	return 0;

end