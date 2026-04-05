create procedure usp_InsertMembershipType
	@Name nvarchar(20),
	@DurationDay smallint,
	@Price money,
	@MembershipTypeId int output
as
begin
	set nocount on;

	if @Name is null or len(@Name) < 3 
	begin
	raiserror ('Membership type name must be min 2 symbols', 16, 2)
	return 1;
	end

	if @DurationDay < 0
	begin
	raiserror ('duration day cannot be negative', 16, 2)
	return 2;
	end

	if @Price < 0 
	begin
	raiserror ('price cannot be negative', 16, 3)
	return 3;
	end

	if exists (select 1 from MembershipTypes where Name = @Name and IsDeleted = 0)
	begin
	raiserror ('membership type name already exists', 16, 3)
	return 4;
	end

	insert into MembershipTypes (Name, DurationDay, Price)
	values (@Name, @DurationDay, @Price);
	set @MembershipTypeId = scope_identity();
	return 0;
end