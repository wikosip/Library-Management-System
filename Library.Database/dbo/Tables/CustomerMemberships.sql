create table CustomerMemberships(
	CustomerMembershipId int primary key identity(1, 1),
	CustomerId int not null references Customers(CustomerId),
	MembershipTypeId int not null references MembershipTypes(MembershipTypeId),
	StartDate datetime not null default getdate(),
	EndDate datetime not null,
	[IsDeleted] bit not null default 0
)