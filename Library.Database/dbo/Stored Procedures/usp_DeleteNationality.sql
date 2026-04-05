create procedure usp_DeleteNationality
    @NationalityId int
as
begin
    set nocount on;

    if not exists (select 1 from Nationalities where NationalityId = @NationalityId and IsDeleted = 0)
    begin
        raiserror('NationalityId does not exist.', 16, 1);
        return 1;
    end

    update Nationalities
    set IsDeleted = 1,
        UpdateDate = getdate()
    where NationalityId = @NationalityId;

    return 0;
end