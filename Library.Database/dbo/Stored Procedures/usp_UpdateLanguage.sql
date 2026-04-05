create procedure usp_UpdateLanguage
    @LanguageId int,
    @Name varchar(30)
as
begin
    set nocount on;

    if not exists (select 1 from Languages where LanguageId = @LanguageId and IsDeleted = 0)
    begin
        raiserror('LanguageId %d does not exist.', 16, 1, @LanguageId);
        return 1;
    end

    if @Name is null or len(@Name) < 3
    begin
        raiserror('Language name cannot be empty.', 16, 1);
        return 2;
    end

    update Languages
    set Name = @Name,
        UpdateDate = getdate()
    where LanguageId = @LanguageId
      and (
            Name != @Name
          );
    return 0;
end