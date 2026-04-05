create table Employees(
	EmployeeId int primary key identity(1,1),
	CityId int not null references Cities(CityId),
	PositionId int not null references Positions(PositionId),
	FirstName nvarchar(30) not null,
	LastName nvarchar(30) not null,
	PersonalNumber char(11) not null check (len(PersonalNumber) = 11),
	Address nvarchar(50) not null,
	PhoneNumber varchar(20) not null,
	Email varchar(50) not null,
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate date null
)