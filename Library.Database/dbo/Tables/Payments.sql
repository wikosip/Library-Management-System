create table Payments(
	PaymentId int primary key identity(1, 1),
	CustomerId int not null references Customers(CustomerId),
	EmployeeId int not null references Employees(EmployeeId),
	PaymentDate date not null default getdate(),
	Amount money not null
)