create procedure usp_ClearDatabase
as
begin
    set nocount on;

    delete from BookLoanPayments;
    delete from MembershipPayments;
    delete from BookLanguages;
    delete from BookAuthors;
    delete from UserRoles;
    delete from RolePermissions;

    delete from BookLoans;
    dbcc checkident ('BookLoans', reseed, 0);

    delete from CustomerMemberships;
    dbcc checkident ('CustomerMemberships', reseed, 0);

    delete from Payments;
    dbcc checkident ('Payments', reseed, 0);

    delete from BookInstances;
    dbcc checkident ('BookInstances', reseed, 0);

    delete from UserAccounts;

    delete from Books;
    dbcc checkident ('Books', reseed, 0);

    delete from Customers;
    dbcc checkident ('Customers', reseed, 0);

    delete from Employees;
    dbcc checkident ('Employees', reseed, 0);

    delete from Authors;
    dbcc checkident ('Authors', reseed, 0);

    delete from MembershipTypes;
    dbcc checkident ('MembershipTypes', reseed, 0);

    delete from Conditions;
    dbcc checkident ('Conditions', reseed, 0);

    delete from Languages;
    dbcc checkident ('Languages', reseed, 0);

    delete from Genres;
    dbcc checkident ('Genres', reseed, 0);

    delete from Publishers;
    dbcc checkident ('Publishers', reseed, 0);

    delete from Nationalities;
    dbcc checkident ('Nationalities', reseed, 0);

    delete from Positions;
    dbcc checkident ('Positions', reseed, 0);

    delete from Permissions;
    dbcc checkident ('Permissions', reseed, 0);

    delete from Roles;
    dbcc checkident ('Roles', reseed, 0);

    delete from Cities;
    dbcc checkident ('Cities', reseed, 0);

    delete from Countries;
    dbcc checkident ('Countries', reseed, 0);

    return 0;
end