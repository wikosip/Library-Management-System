create procedure usp_InsertPayment 
@CustomerId int,
@EmployeeId int,
@Amount int,
@PaymentId int output
as
begin
	set nocount on;

	if @Amount < 0
		begin
		raiserror ('Amount cannot be a negative', 16, 3);
		return 3;
	end

	if not exists(select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
		raiserror ('Invalid CustomerId', 16, 2);
		return 1;
	end

	if not exists(select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
	begin
		raiserror ('Invalid EmployeeId', 16, 3);
		return 2;
	end

	insert into Payments(CustomerId, EmployeeId, Amount)
	values (@CustomerId, @EmployeeId, @Amount)

	set @PaymentId = SCOPE_IDENTITY();
	return 0;
end