create procedure usp_UpdateCustomerMembership
    @CustomerMembershipId int,
    @MembershipTypeId int,
	@CustomerId int,
    @EndDate date
as
begin
    set nocount on;

	if not exists (select 1 from CustomerMemberships where CustomerMembershipId = @CustomerMembershipId and IsDeleted = 0)
	begin
    raiserror('CustomerMembershipId %d does not exist.', 16, 1, @CustomerMembershipId);
    return 1;
    end

	if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
	raiserror ('CustomerId does not exists', 16, 1);
	return 2;
	end

	if not exists (select 1 from MembershipTypes where MembershipTypeId = @MembershipTypeId and IsDeleted = 0)
	begin
	raiserror ('MembershipId does not exist', 16, 1)
	return 3;
	end

	update CustomerMemberships
	 set
		CustomerId = @CustomerId,
		MembershipTypeId = @MembershipTypeId,
        EndDate = @EndDate
    where CustomerMembershipId = @CustomerMembershipId and
		(CustomerId != @CustomerId or
		MembershipTypeId != @MembershipTypeId or
		EndDate != @EndDate)

      return 0 
end