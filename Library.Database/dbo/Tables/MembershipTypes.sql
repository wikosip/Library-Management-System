create table MembershipTypes(
	MembershipTypeId int primary key identity(1, 1),
	Name nvarchar(20) not null,
	DurationDay smallint not null check(DurationDay between 1 and 300),
	Price money not null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)