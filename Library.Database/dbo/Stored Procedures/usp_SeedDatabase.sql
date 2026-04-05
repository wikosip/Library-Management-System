create procedure usp_SeedDatabase
as
begin
	set nocount on;

	insert into Countries (Name, IsoName) values ('United States', 'US');
	insert into Countries (Name, IsoName) values ('United Kingdom', 'UK');
	insert into Countries (Name, IsoName) values ('Australia', 'AU');

	insert into Cities (Name, CountryId, IsoName) values ('New York', 1, 'NY');
	insert into Cities (Name, CountryId, IsoName) values ('Los Angeles', 1, 'LA');
	insert into Cities (Name, CountryId, IsoName) values ('London', 2, 'LDN');
	insert into Cities (Name, CountryId, IsoName) values ('Manchester', 2, 'MAN');
	insert into Cities (Name, CountryId, IsoName) values ('Sydney', 3, 'SYD');
	insert into Cities (Name, CountryId, IsoName) values ('Melbourne', 3, 'MEL');

	insert into Positions (Name, Description) values ('Librarian', 'Responsible for managing library resources');
	insert into Positions (Name, Description) values ('Cashier', 'Handles payments and membership fees');
	insert into Positions (Name, Description) values ('Manager', 'Oversees library operations');

	insert into Employees (CityId, PositionId, FirstName, LastName, PersonalNumber, Address, PhoneNumber, Email) values (1, 1, 'John', 'Doe', '12345678901', '123 Main St', '555-1234', 'john.doe@example.com');
	insert into Employees (CityId, PositionId, FirstName, LastName, PersonalNumber, Address, PhoneNumber, Email) values (2, 2, 'Jane', 'Smith', '23456789012', '456 Oak St', '555-5678', 'jane.smith@example.com');
	insert into Employees (CityId, PositionId, FirstName, LastName, PersonalNumber, Address, PhoneNumber, Email) values (3, 3, 'Alice', 'Johnson', '34567890123', '789 Pine St', '555-9012', 'alice.johnson@example.com');

	insert into Customers (CityId, PersonalNumber, FirstName, LastName, Address, Email, PhoneNumber) values (1, '98765432101', 'Michael', 'Brown', '111 Elm St', 'michael.brown@example.com', '555-0001');
	insert into Customers (CityId, PersonalNumber, FirstName, LastName, Address, Email, PhoneNumber) values (2, '87654321012', 'Sarah', 'Davis', '222 Maple St', 'sarah.davis@example.com', '555-0002');
	insert into Customers (CityId, PersonalNumber, FirstName, LastName, Address, Email, PhoneNumber) values (3, '76543210123', 'David', 'Wilson', '333 Birch St', 'david.wilson@example.com', '555-0003');

	insert into Genres (Name) values ('Science Fiction');
	insert into Genres (Name) values ('Fantasy');
	insert into Genres (Name) values ('Mystery');

	insert into Publishers (Name) values ('Penguin Random House');
	insert into Publishers (Name) values ('HarperCollins');
	insert into Publishers (Name) values ('Simon & Schuster');

	insert into Nationalities (Name) values ('American');
	insert into Nationalities (Name) values ('Canadian');
	insert into Nationalities (Name) values ('German');

	insert into Authors (NationalityId, FirstName, LastName) values (1, 'Isaac', 'Asimov');
	insert into Authors (NationalityId, FirstName, LastName) values (2, 'Margaret', 'Atwood');
	insert into Authors (NationalityId, FirstName, LastName) values (3, 'Hermann', 'Hesse');

	insert into Languages (Name) values ('English');
	insert into Languages (Name) values ('German');
	insert into Languages (Name) values ('French');

	insert into Books (GenreId, PublisherId, Title, ISBN, PublicationYear, PageCount) values (1, 1, 'Foundation', '978-0-553-80371-0', 1951, 255);
	insert into Books (GenreId, PublisherId, Title, ISBN, PublicationYear, PageCount) values (2, 2, 'The Handmaid''s Tale', '978-0-06-112495-2', 1985, 311);
	insert into Books (GenreId, PublisherId, Title, ISBN, PublicationYear, PageCount) values (3, 3, 'Siddhartha', '978-0-679-60139-5', 1922, 152);

	insert into Conditions (Name, Value) values ('New', 5);
	insert into Conditions (Name, Value) values ('Good', 4);
	insert into Conditions (Name, Value) values ('Worn', 2);

	insert into BookInstances (BookId, ConditionId) values (1, 1);
	insert into BookInstances (BookId, ConditionId) values (2, 2);
	insert into BookInstances (BookId, ConditionId) values (3, 3);

	insert into MembershipTypes (Name, DurationDay, Price) values ('Basic', 30, 10.00);
	insert into MembershipTypes (Name, DurationDay, Price) values ('Standard', 90, 25.00);
	insert into MembershipTypes (Name, DurationDay, Price) values ('Premium', 180, 45.00);

	insert into CustomerMemberships (CustomerId, MembershipTypeId, EndDate) values (1, 1, '2026-04-16');
	insert into CustomerMemberships (CustomerId, MembershipTypeId, EndDate) values (2, 2, '2026-06-16');
	insert into CustomerMemberships (CustomerId, MembershipTypeId, EndDate) values (3, 3, '2026-09-16');

	insert into Payments (CustomerId, EmployeeId, Amount) values (1, 1, 10.00);
	insert into Payments (CustomerId, EmployeeId, Amount) values (2, 2, 25.00);
	insert into Payments (CustomerId, EmployeeId, Amount) values (3, 3, 45.00);

	insert into BookLoans (BookInstanceId, ConditionId, CustomerId, UnitPrice, Discount, EndDate) values (1, 1, 1, 2.50, 0, '2026-03-30');
	insert into BookLoans (BookInstanceId, ConditionId, CustomerId, UnitPrice, Discount, EndDate) values (2, 2, 2, 3.00, 0.5, '2026-04-01');
	insert into BookLoans (BookInstanceId, ConditionId, CustomerId, UnitPrice, Discount, EndDate) values (3, 3, 3, 1.50, 0, '2026-03-28');

	insert into Roles (Name) values ('Admin');
	insert into Roles (Name) values ('Employee');
	insert into Roles (Name) values ('Customer');

	insert into Permissions (Name) values ('Manage Books');
	insert into Permissions (Name) values ('Manage Customers');
	insert into Permissions (Name) values ('Process Payments');

	return 0;
end