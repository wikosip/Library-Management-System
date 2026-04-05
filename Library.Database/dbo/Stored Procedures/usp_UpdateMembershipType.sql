create procedure usp_UpdateMembershipType
	@MembershipTypeId int,
	@Name nvarchar(20),
	@DurationDay smallint,
	@Price money
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

	if exists (select 1 from MembershipTypes where Name = @Name and MembershipTypeId != @MembershipTypeId and IsDeleted = 0)
	begin
	raiserror ('membership type name already exists', 16, 3)
	return 4;
	end

	if not exists (select 1 from MembershipTypes where MembershipTypeId = @MembershipTypeId and IsDeleted = 0)
	begin
	raiserror('MembershipTypeId %d does not exist.', 16, 1, @MembershipTypeId);
	return 5;
	end

	update MembershipTypes
	set Name = @Name,
		DurationDay = @DurationDay,
		Price = @Price,
		UpdateDate = getdate()
	where MembershipTypeId = @MembershipTypeId
	    and (Name != @Name 
		or DurationDay != @DurationDay 
		or Price != @Price);

	return 0;
end