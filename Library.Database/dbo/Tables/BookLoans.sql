create table BookLoans(
	BookLoanId int primary key identity(1, 1),
	BookInstanceId int not null references BookInstances(BookInstanceId),
	ConditionId int not null references Conditions(ConditionId),
	CustomerId int not null references Customers(CustomerId),
	UnitPrice money not null,
	Discount real NOT null,
    StartDate datetime not null default getdate(),
	EndDate datetime not null,
	RealEndDate datetime null,
	FineAmount money null,
	IsReturned bit not null default 0
)