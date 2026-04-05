create table Countries(
	CountryId int primary key identity (1, 1),
	Name nvarchar(50) not null unique,
	IsoName varchar(5) not null unique,
	Flag varbinary(max) null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)