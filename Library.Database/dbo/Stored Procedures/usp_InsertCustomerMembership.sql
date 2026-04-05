create procedure usp_InsertCustomerMembership
	@CustomerId int,
	@MembershipTypeId int,
    @EndDate date,
	@CustomerMembershipId int output
as
begin 
    set nocount on;

	if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
	raiserror ('CustomerId does not exists', 16, 1);
	return 1;
	end

	if not exists (select 1 from MembershipTypes where MembershipTypeId = @MembershipTypeId and IsDeleted = 0)
	begin
	raiserror ('MembershipId does not exists', 16, 1)
	return 2;
	end

	insert into CustomerMemberships(CustomerId, MembershipTypeId, EndDate)
    values (@CustomerId, @MembershipTypeId, @EndDate);
	set @CustomerMembershipId = scope_identity();
	return 0;
end