create table BookInstances(
	BookInstanceId int primary key identity(1, 1),
	BookId int not null references Books(BookId),
	ConditionId int not null references Conditions(ConditionId),
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)