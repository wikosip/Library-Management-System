create procedure usp_InsertMembershipPayment
@CustomerMembershipId int,
@PaymentId int output
as
begin
	set nocount on;

	if not exists (select 1 from CustomerMemberships where CustomerMembershipId = @CustomerMembershipId and IsDeleted = 0)
	begin
		raiserror ('Invalid CustomerMembershipId', 16, 2);
		return 1;
	end

	if not exists (select 1 from Payments where PaymentId = @PaymentId)
	begin
		raiserror ('Invalid PaymentId', 16, 2)
		return 2;
	end

	insert into MembershipPayments(CustomerMembershipId, PaymentId)
	values (@CustomerMembershipId, @PaymentId)

	return 0;
end