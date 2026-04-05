create table Positions(
	PositionId int primary key identity (1, 1),
	Name nvarchar(50) not null unique,
	Description nvarchar(250) not null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)