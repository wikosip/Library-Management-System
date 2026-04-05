create procedure usp_DeleteCustomer
@CustomerId int
as
begin
	set nocount on;

	if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
	raiserror('CustomerId does not exist.', 16, 1, @CustomerId);
	return 1;
	end

	update Customers
	set IsDeleted = 1,
	UpdateDate = getdate()
	where CustomerId = @CustomerId;

	return 0;
end