create procedure usp_UpdateBookLoan
	@BookLoanId int,
	@BookInstanceId int,
	@ConditionId int,
    @CustomerId int,
    @UnitPrice money,
    @Discount real,
	@RealEndDate date,
	@FineAmount decimal,
	@IsReturned bit
as
 begin
 set nocount on;

   if @UnitPrice < 0
   begin
   raiserror ('Unit price can not be negative',16,1)
   return 1;
   end
   
   if @Discount not between 0 and 100
   begin
   raiserror ('invalid discount',16,1)
   return 2;
   end

   if not exists (select 1 from BookInstances where BookInstanceId = @BookInstanceId and IsDeleted = 0)
   begin
    raiserror (' Invalid book instance id ',16,1)
    return 4;
   end

   if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
   begin
    raiserror (' Invalid Customer id ',16,1)
    return 5;
   end

   if not exists (select 1 from BookLoans where BookLoanId = @BookLoanId and IsReturned = 0)
   begin
	raiserror ('Invalid BookloanId', 16, 2)
	return 6;
   end

   update BookLoans 
   set BookInstanceId = @BookInstanceId,
	   CustomerId = @CustomerId,
	   UnitPrice = @UnitPrice,
	   Discount = @Discount,
	   ConditionId = @ConditionId,
	   RealEndDate = @RealEndDate,
	   FineAmount =  @FineAmount,
	   IsReturned = @IsReturned
	   where BookLoanId = @BookLoanId and 
	   (CustomerId != @CustomerId or
	   UnitPrice != @UnitPrice or 
	   Discount != @Discount or
	   ConditionId != @ConditionId or
	   RealEndDate != @RealEndDate or
	   FineAmount !=  @FineAmount or
	   IsReturned != @IsReturned
	   )

	   return 0;
 end