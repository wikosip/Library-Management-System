create procedure usp_DeleteCustomerMembership
@CustomerMembershipId int
as
begin
	if not exists (select 1 from CustomerMemberships where CustomerMembershipId = @CustomerMembershipId and IsDeleted = 0)
	begin
	raiserror ('CustomerMembershipId does not exist', 16, 2)
	return 1;
	end

	update CustomerMemberships
	set IsDeleted = 1
	where CustomerMembershipId = @CustomerMembershipId

	return 0;
end