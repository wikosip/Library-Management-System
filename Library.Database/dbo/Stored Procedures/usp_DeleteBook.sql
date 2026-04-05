create procedure usp_DeleteBook
	@BookId int
as
begin
	set nocount on;

	if not exists(select 1 from Books where BookId = @BookId and IsDeleted = 0)
	begin
		raiserror('BookId %d does not exist.', 16, 1, @BookId);
		return 1;
	end

	update Books
	set IsDeleted = 1,
		UpdateDate = getdate()
	where BookId = @BookId;

	return 0;
end