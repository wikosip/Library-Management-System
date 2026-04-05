create procedure usp_InsertGenre
@Name nvarchar(50),
@GenreId int output
as
begin
	set nocount on;

	if @Name is null or len(@Name) < 3 
	begin
	raiserror ('genre name must be min 3 symbols', 16, 1);
	return 1;
	end

	if exists (select 1 from Genres where Name = @Name and IsDeleted = 0)
	begin
	raiserror('Genre with name already exists.', 14, 1, @Name);
	return 2;
	end

	insert into Genres (Name)
	values (@Name);
    set @GenreId = scope_identity();
	return 0;
end