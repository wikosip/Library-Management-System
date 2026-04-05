create procedure usp_UpdateGenre
@GenreId int,
@Name nvarchar(50)
as
begin
	set nocount on;

	if @Name is null or len(@Name) < 3 
	begin
	raiserror ('genre name must be min 3 symbols', 16, 1);
	return 1;
	end

	if not exists (select 1 from Genres where GenreId = @GenreId and IsDeleted = 0)
	begin
	raiserror('GenreId does not exist.', 16, 1, @GenreId);
	return 2;
	end

	if exists (select 1 from Genres where Name = @Name and IsDeleted = 0 and GenreId != @GenreId and IsDeleted = 0)
	begin
	raiserror('Genre with name already exists.', 14, 1, @Name);
	return 3;
	end

	update Genres
	set Name = @Name,
	UpdateDate = getdate()
	where GenreId = @GenreId
	and Name != @Name;
	return 0;
end