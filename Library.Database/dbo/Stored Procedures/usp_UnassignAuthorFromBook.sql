create procedure usp_UnassignAuthorFromBook
	@BookId int,
	@AuthorId int
as
begin
	set nocount on;

	if not exists(select 1 from BookAuthors
				  where BookId = @BookId and AuthorId = @AuthorId)
	begin
		raiserror('Author is already not assigned to this book.', 16, 1);
		return 1;
	end

	delete from BookAuthors
	where BookId = @BookId and AuthorId = @AuthorId

	return 0;
end