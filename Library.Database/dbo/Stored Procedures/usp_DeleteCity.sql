create procedure usp_DeleteCity
	@CityId int
as
begin
	set nocount on;

	if not exists (select 1 from Cities where CityId = @CityId and IsDeleted = 0)
	begin
	raiserror('@CityId does not exist.', 16, 1, @CityId);
	return 1;
	end

	update Cities
	set IsDeleted = 1,
		UpdateDate = getdate()
	where CityId = @CityId ;

	return 0;
end