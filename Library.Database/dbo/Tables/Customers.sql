create table Customers(
	CustomerId int primary key identity(1, 1),
	CityId int not null references Cities(CityId),
	PersonalNumber char(11) not null check (len(PersonalNumber) = 11),
	FirstName nvarchar(20) not null,
	[LastName] nvarchar(30) not null,
	BirthDate date null,
	Address nvarchar (50) null,
	Email varchar(50) null,
	PhoneNumber varchar(20),
	IsDeleted bit not null default 0,
	CreateDate date not null default getdate(),
	UpdateDate DATETIME2 null,
	check (Address is not null or PhoneNumber is not null or Email is not null)
)