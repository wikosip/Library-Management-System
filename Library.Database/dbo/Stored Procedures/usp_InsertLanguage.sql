create procedure usp_InsertLanguage
    @Name varchar(30),
    @LanguageId int output
as
begin
    set nocount on;

    if @Name is null or len(@Name) < 3
    begin
        raiserror('Language name cannot be empty.', 16, 1);
        return 1;
    end

    if exists (select 1 from Languages where Name = @Name and IsDeleted = 0)
    begin
        raiserror('Language with name "%s" already exists.', 16, 1, @Name);
        return 2;
    end

    if exists (select 1 from Languages where Name = @Name and IsDeleted = 1)
    begin
        raiserror('Language with name "%s" has been deleted.', 16, 1, @Name);
        return 3;
    end

    insert into Languages (Name)
    values (@Name);

    set @LanguageId = scope_identity();
    return 0;
end