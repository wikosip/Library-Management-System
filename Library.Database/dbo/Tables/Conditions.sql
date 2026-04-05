create table Conditions(
	ConditionId int primary key identity(1, 1),
	Name nvarchar(20) not null unique,
	Value tinyint not null check (Value between 1 and 5),
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)