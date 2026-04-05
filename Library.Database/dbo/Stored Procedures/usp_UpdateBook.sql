create procedure usp_UpdateBook
	@BookId int,
	@GenreId int,
	@PublisherId int,
	@Title nvarchar(200),
	@ISBN varchar(20),
	@PublicationYear smallint = null,
	@PageCount smallint = null
as
begin
	set nocount on;

	if not exists(select 1 from Books where BookId = @BookId and IsDeleted = 0)
	begin
		raiserror('BookId %d does not exist.', 16, 1, @BookId);
		return 1;
	end

	if exists(select 1 from Books where ISBN = @ISBN and BookId != @BookId and IsDeleted = 0)
	begin
		raiserror('ISBN "%s" already exists for another book.', 16, 1, @ISBN);
		return 2;
	end

	if not exists(select 1 from Genres where GenreId = @GenreId)
	begin
		raiserror('GenreId %d does not exist.', 16, 1, @GenreId);
		return 3;
	end
	
	if not exists(select 1 from Publishers where PublisherId = @PublisherId)
	begin
		raiserror('PublisherId %d does not exist.', 16, 1, @PublisherId);
		return 4;
	end

	if (@ISBN) is null or (@Title) is null
	begin 		
	raiserror('ISBN or Title cannot be null.', 16, 1);
		return 5;
	end

	update Books
	set GenreId = @GenreId,
		PublisherId = @PublisherId,
		Title = @Title,
		ISBN = @ISBN,
		PublicationYear = @PublicationYear,
		PageCount = @PageCount,
		UpdateDate = getdate()
	where BookId = @BookId and 
		  (GenreId != @GenreId or
		  PublisherId != @PublisherId or
		  Title != @Title or
		  ISBN != @ISBN or
		  (PublicationYear is null and @PublicationYear is not null) or
		  (PublicationYear is not null and @PublicationYear is null) or
		  PublicationYear != @PublicationYear or
		  (PageCount is null and @PageCount is not null) or
		  (PageCount is not null and @PageCount is null) or
		  PageCount != @PageCount);

	return 0;
end