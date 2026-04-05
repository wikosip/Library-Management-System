create table Authors(
	AuthorId int primary key identity(1, 1),
	NationalityId int null references Nationalities(NationalityId),
	FirstName nvarchar(20) not null,
	LastName nvarchar(30) not null,
	BirthDate date null,
	DeathDate date null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)