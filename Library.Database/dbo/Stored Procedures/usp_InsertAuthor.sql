create procedure usp_InsertAuthor
    @NationalityId int = null,
    @FirstName nvarchar(20),
    @LastName nvarchar(30),
    @BirthDate date = null,
    @DeathDate date = null,
    @AuthorId int output
as
begin
    set nocount on;

    if @FirstName is null or len(@FirstName) = 0
    begin
        raiserror('FirstName cannot be empty.', 16, 1);
        return 1;
    end

    if @LastName is null or len(@LastName) = 0
    begin
        raiserror('LastName cannot be empty.', 16, 1);
        return 1;
    end

    if @NationalityId is not null
    begin
         if not exists (select 1 from Nationalities where NationalityId = @NationalityId and IsDeleted = 0)
         begin
              raiserror('NationalityId %d does not exist.', 16, 1, @NationalityId);
              return 2;
         end
    end

    if @BirthDate is not null and @BirthDate > getdate()
    begin
        raiserror('BirthDate cannot be in the future.', 16, 1);
        return 3;
    end

    if @DeathDate is not null
    begin
        if @BirthDate is not null and @DeathDate < @BirthDate
        begin
            raiserror('DeathDate cannot be earlier than BirthDate.', 16, 1);
            return 1;
        end

        if @DeathDate > getdate()
        begin
            raiserror('DeathDate cannot be in the future.', 16, 1);
            return 1;
        end
    end
   
    if exists (
         select 1 from Authors 
           where
               FirstName = @FirstName
      and LastName = @LastName
      and ((@BirthDate is null and BirthDate is null) or BirthDate = @BirthDate)
      and ((@DeathDate is null and DeathDate is null) or DeathDate = @DeathDate)
      and IsDeleted = 0
    )
    begin
        raiserror('Author with the same first name, last name, birth and death date and delete status already exists.', 16, 1);
        return 1;
    end

    insert into Authors (NationalityId, FirstName, LastName, BirthDate, DeathDate)
    values (@NationalityId, @FirstName, @LastName, @BirthDate, @DeathDate);

    set @AuthorId = scope_identity();
    return 0;
end