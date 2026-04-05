create procedure usp_DeleteGenre
@GenreId int
as
begin
	set nocount on;

	if not exists (select 1 from Genres where GenreId = @GenreId and IsDeleted = 0)
	begin
	raiserror('GenreId does not exist.', 16, 1, @GenreId);
	return 1;
	end

	update Genres
	set IsDeleted = 1,
	UpdateDate = getdate()
	where GenreId = @GenreId;
	return 0;
end