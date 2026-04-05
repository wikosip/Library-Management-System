create table UserAccounts (
	EmployeeId int primary key references Employees(EmployeeId),
	Username varchar(50) not null unique,
	Password varbinary(255) not null,
	IsActive bit not null default 1,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)