create procedure usp_DeleteCountry
	@CountryId int
as
begin
	set nocount on;

	if not exists (select 1 from Countries where CountryId = @CountryId and IsDeleted = 0)
	begin
		raiserror('@CountryId does not exist.', 16, 1, @CountryId );
		return 1;
	end

	update Countries
	set IsDeleted = 1,
		UpdateDate = getdate()
	where CountryId = @CountryId;

	return 0;
end