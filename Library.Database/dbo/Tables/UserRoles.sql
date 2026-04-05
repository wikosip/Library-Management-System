create table UserRoles (
	UserId int not null references UserAccounts(EmployeeId),
	RoleId int not null references Roles(RoleId),
	primary key (UserId, RoleId)
)