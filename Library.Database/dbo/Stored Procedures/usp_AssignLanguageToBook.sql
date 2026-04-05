create procedure usp_AssignLanguageToBook
	@BookId int,
	@LanguageId int
as
begin
	set nocount on;
	
	if not exists(select 1 from Books where BookId = @BookId and IsDeleted = 0)
	begin
		raiserror('BookId %d does not exist or has been deleted.', 16, 1, @BookId);
		return 1;
	end
	
	if not exists(select 1 from Languages where LanguageId = @LanguageId)
	begin
		raiserror('LanguageId %d does not exist.', 16, 1, @LanguageId);
		return 2;
	end
	
	if exists(select 1 from BookLanguages 
	           where BookId = @BookId and LanguageId = @LanguageId)
	begin
		raiserror('Language already assigned to this book.', 16, 1);
		return 3;
	end
	
	insert into BookLanguages (BookId, LanguageId)
	values (@BookId, @LanguageId);
	
	return 0;
end