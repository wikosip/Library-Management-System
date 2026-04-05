create table BookAuthors(
	BookId int not null references Books(BookId),
	AuthorId int not null references Authors(AuthorId),
	primary key (BookId, AuthorId)
)