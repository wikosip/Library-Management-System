create procedure usp_UpdateCountry
	@CountryId int,
	@Name nvarchar(50),
	@IsoName varchar(5)
as
begin
	set nocount on;

	if @IsoName is null or LEN(@IsoName) < 2
	begin
	raiserror('Iso name must be at least 2 characters long.', 16, 1, @CountryId);
	return 1;
	end

	if @Name is null or LEN(@Name) < 2
	begin
	raiserror('Country name must be at least 2 characters long.', 16, 1, @CountryId);
	return 2;
	end
	
	if not exists (select 1 from Countries where CountryId = @CountryId and IsDeleted = 0)
	begin
	raiserror('CountryId does not exist.', 16, 1, @CountryId);
	return 3;
	end

	if exists (select 1 from Countries where Name = @Name and CountryId != @CountryId and IsDeleted = 0)
	begin
	raiserror('Country name already exists', 16, 2)
	return 4;
	end

    if exists (select 1 from Countries where IsoName = @IsoName and CountryId != @CountryId and IsDeleted = 0)
	begin
	raiserror('IsoName already exists', 16, 1, @IsoName);
	return 5;
	end

	update Countries
	set Name = @Name,
		IsoName = @IsoName,
		UpdateDate = getdate()
	where CountryId = @CountryId and 
		  (Name != @Name or 
		     IsoName != @IsoName);

	return 0;

end