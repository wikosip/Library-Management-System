create procedure usp_UpdatePayment
@PaymentId int,
@CustomerId int,
@EmployeeId int,
@Amount int
as
begin
	if @Amount < 0
		begin
		raiserror ('Amount cannot be a negative', 16, 3);
		return 1;
	end

	if not exists(select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
	begin
		raiserror ('Invalid CustomerId', 16, 2);
		return 2;
	end

	if not exists(select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
	begin
		raiserror ('Invalid EmployeeId', 16, 3);
		return 3;
	end

	if not exists(select 1 from Payments where PaymentId = @PaymentId and @Amount > 0)
	begin
		raiserror ('Invalid PaymentId', 16, 2);
		return 4;
	end

	update Payments
	set CustomerId = @CustomerId,
		EmployeeId = @EmployeeId,
		Amount = @Amount
		where PaymentId = @PaymentId and
		(CustomerId != @CustomerId or
		EmployeeId != @EmployeeId or
		Amount != @Amount
		)

	return 0;
end