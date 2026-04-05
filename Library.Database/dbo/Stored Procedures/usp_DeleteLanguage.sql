create procedure usp_DeleteLanguage
    @LanguageId int
as
begin
    set nocount on;

    if not exists (select 1 from Languages where LanguageId = @LanguageId and IsDeleted = 0)
    begin
        raiserror('LanguageId %d does not exist in the database.', 16, 1, @LanguageId);
        return 1;
    end

    if exists (select 1 from Languages where LanguageId = @LanguageId and IsDeleted = 1)
    begin
        raiserror('LanguageId %d is already deleted.', 16, 1, @LanguageId);
        return 1;
    end

    update Languages
    set IsDeleted = 1,
        UpdateDate = getdate()
    where LanguageId = @LanguageId;

    return 0;
end