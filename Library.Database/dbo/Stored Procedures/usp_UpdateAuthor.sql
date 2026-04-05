create procedure usp_UpdateAuthor
@AuthorId int,
@NationalityId int = null,
@FirstName nvarchar(20),
@LastName nvarchar(30),
@BirthDate date = null,
@DeathDate date = null
as
begin
    set nocount on;

    if not exists (select 1 from Authors where AuthorId = @AuthorId and IsDeleted = 0)
    begin
        raiserror('AuthorId %d does not exist.', 16, 1, @AuthorId);
        return 1;
    end

    if @FirstName is null or len(@FirstName) < 3
    begin
        raiserror('FirstName cannot be empty.', 16, 1);
        return 2;
    end

    if @LastName is null or len(@LastName) < 3
    begin
        raiserror('LastName cannot be empty.', 16, 1);
        return 3;
    end
    
    if @NationalityId is not null
    begin
         if not exists (select 1 from Nationalities where NationalityId = @NationalityId and IsDeleted = 0)
         begin
              raiserror('NationalityId %d does not exist.', 16, 1, @NationalityId);
              return 4;
         end
    end

    if @BirthDate is not null and @BirthDate > getdate()
    begin
        raiserror('BirthDate cannot be in the future.', 16, 1);
        return 5;
    end

    if @DeathDate is not null
    begin
        if @BirthDate is not null and @DeathDate < @BirthDate
        begin
            raiserror('DeathDate can not be earlier than BirthDate.', 16, 1);
            return 6;
        end

        if @DeathDate > getdate()
        begin
            raiserror('DeathDate can not be in the future.', 16, 1);
            return 7;
        end
    end

    update Authors
    set NationalityId = @NationalityId,
        FirstName = @FirstName,
        LastName = @LastName,
        BirthDate = @BirthDate,
        DeathDate = @DeathDate,
        UpdateDate = getdate()
    where AuthorId = @AuthorId
      and (
            NationalityId != @NationalityId or 
            (NationalityId is null and @NationalityId is not null) or
            (NationalityId is not null and @NationalityId is null) or

            FirstName != @FirstName or
            LastName != @LastName or

            BirthDate != @BirthDate or
            (BirthDate is null and @BirthDate is not null) or
            (BirthDate is not null and @BirthDate is null) or

            DeathDate != @DeathDate or
            (DeathDate is null and @DeathDate is not null) or
            (DeathDate is not null and @DeathDate is null)
          );

    return 0;
end