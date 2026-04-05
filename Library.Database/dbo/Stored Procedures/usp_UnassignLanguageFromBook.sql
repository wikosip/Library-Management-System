create procedure usp_UnassignLanguageFromBook
	@BookId int,
	@LanguageId int
as
begin
	set nocount on;

	if not exists(select 1 from BookLanguages
				  where BookId = @BookId and LanguageId = @LanguageId)
	begin
		raiserror('Language is already not assigned to this book.', 16, 1);
		return 1;
	end

	delete from BookLanguages
	where BookId = @BookId and LanguageId = @LanguageId;

	return 0;
end