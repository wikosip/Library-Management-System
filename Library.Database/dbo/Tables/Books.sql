create table Books(
	BookId int primary key identity(1, 1),
	GenreId int not null references Genres(GenreId),
	PublisherId int not null references Publishers(PublisherId),
	Title nvarchar(200) not null,
	ISBN varchar(20) not null unique,
	PublicationYear smallint null,
	PageCount smallint null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)