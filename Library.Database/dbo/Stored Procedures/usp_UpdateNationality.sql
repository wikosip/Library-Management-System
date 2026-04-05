create procedure usp_UpdateNationality
    @NationalityId int,
    @Name nvarchar(20)
as
begin
    set nocount on;

	if @Name is null or len(@Name) < 3
	begin
	raiserror ('Nationality name must be min 2 symbols', 16, 2);
	return 1;
	end

	if exists (select 1 from Nationalities where Name = @Name and NationalityId != @NationalityId and IsDeleted = 0)
	begin
	raiserror ('nationality name already exist', 16, 2)
	return 2;
	end

    if not exists (select 1 from Nationalities where NationalityId = @NationalityId and IsDeleted = 0)
    begin
    raiserror('NationalityId does not exist.', 16, 1);
    return 1;
    end

    update Nationalities
    set Name = @Name,
        UpdateDate = getdate()
    where NationalityId = @NationalityId and
		  Name != @Name;

    return 0;
end