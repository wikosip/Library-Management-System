create table Languages(
	LanguageId int primary key identity(1, 1),
	Name varchar(30) not null unique,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)