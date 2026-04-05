create procedure usp_InsertCountry
	@Name nvarchar(50),
	@IsoName varchar(5),
	@CountryId int output
as
begin
	set nocount on;

	 if LEN(@Name) < 2
	begin
	raiserror('Country name must be at least 2 characters long.', 16, 1, @CountryId);
	return 1;
	end

	 if LEN(@IsoName) < 2
	begin
	raiserror('Iso name must be at least 2 characters long.', 16, 1, @CountryId);
	return 2;
	end

	if exists(select 1 from Countries where Name = @Name and IsDeleted = 0)
	begin
	raiserror ('Country name already exists', 16, 2)
	return 3;
	end

	if exists (select 1 from Countries where IsoName = @IsoName and IsDeleted = 0)
	begin
	raiserror('IsoName already exists', 16, 1, @IsoName);
	return 4;
	end

	insert into Countries (Name, IsoName)
	values (@Name, @IsoName);
	set @CountryId = scope_identity();

	return 0;
end