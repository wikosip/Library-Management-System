create procedure usp_AssignAuthorToBook
	@BookId int,
	@AuthorId int
as
begin
	set nocount on;

	if not exists(select 1 from Books where BookId = @BookId and IsDeleted = 0)
	begin
		raiserror('BookId %d does not exist or has been deleted.', 16, 1, @BookId);
		return 1;
	end
	
	if not exists(select 1 from Authors where AuthorId = @AuthorId)
	begin
		raiserror('AuthorId %d does not exist.', 16, 1, @AuthorId);
		return 2;
	end

	if exists(select 1 from BookAuthors
			  where BookId = @BookId and AuthorId = @AuthorId)
	begin
		raiserror('Author already assigned to this book.', 16, 1);
		return 3;
	end

	insert into BookAuthors (BookId, AuthorId)
	values (@BookId, @AuthorId);

	return 0;
end