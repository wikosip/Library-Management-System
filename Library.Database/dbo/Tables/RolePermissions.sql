create table RolePermissions (
	RoleId int not null references Roles(RoleId),
	PermissionId int not null references Permissions(PermissionId),
	primary key (RoleId, PermissionId)
)