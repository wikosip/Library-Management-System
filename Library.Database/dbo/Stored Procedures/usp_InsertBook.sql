create procedure usp_InsertBook
	@GenreId int,
	@PublisherId int,
	@Title nvarchar(200),
	@ISBN varchar(20),
	@PublicationYear smallint = null,
	@PageCount smallint = null,
	@BookId int output
as
begin
	set nocount on;

	if @Title is null or len(trim(@Title)) = 0
	begin
		raiserror('Title cannot be empty.', 16, 1);
		return 1;
	end

	if @ISBN is null or len(trim(@ISBN)) = 0
	begin
		raiserror('ISBN cannot be empty.', 16, 1);
		return 2;
	end

	if @PublicationYear is not null and @PublicationYear not between 250 and year(getdate())
	begin
		raiserror('Publication year is invalid.', 16, 1);
		return 3;
	end

	if @PageCount is not null and @PageCount not between 1 and 2500
	begin
		raiserror('Page count is invalid.', 16, 1);
		return 4;
	end

	if exists(select 1 from Books where ISBN = @ISBN and IsDeleted = 0)
	begin
		raiserror('A book with this ISBN already exists.', 16, 1);
		return 5;
	end

	if not exists(select 1 from Genres where GenreId = @GenreId)
	begin
		raiserror('Invalid GenreId.', 16, 1);
		return 6;
	end

	if not exists(select 1 from Publishers where PublisherId = @PublisherId)
	begin
		raiserror('Invalid PublisherId', 16, 1);
		return 7;
	end

	insert into Books (GenreId, PublisherId, Title, ISBN, PublicationYear, PageCount)
	values (@GenreId, @PublisherId, @Title, @ISBN, @PublicationYear, @PageCount);
	set @BookId = scope_identity();

	return 0;
end