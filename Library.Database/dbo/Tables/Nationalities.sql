create table Nationalities(
	NationalityId int primary key identity(1, 1),
	Name nvarchar(20) not null unique,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)