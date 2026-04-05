create table BookLanguages(
	BookId int not null references Books(BookId),
	LanguageId int not null references Languages(LanguageId),
	primary key (BookId, LanguageId)
)