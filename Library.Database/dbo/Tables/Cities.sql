create table Cities(
	CityId int primary key identity(1, 1),
	CountryId int not null references Countries(CountryId),
	Name nvarchar(50) not null,
	IsoName varchar(5) not null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)