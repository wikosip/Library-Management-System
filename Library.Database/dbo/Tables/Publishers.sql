create table Publishers(
	PublisherId int primary key identity(1, 1),
	Name nvarchar(200) not null unique,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)