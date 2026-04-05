create procedure usp_InsertBookLoan
    @BookInstanceId int,
    @CustomerId int,
    @UnitPrice money,
    @Discount real,
    @EndDate date,
    @ConditionId int,
    @BookLoanId int output
as
 begin
 set nocount on;

   if not exists (select 1 from BookInstances where BookInstanceId = @BookInstanceId and IsDeleted = 0)
   begin
    raiserror ('Invalid book instance id ',16,1)
    return 1
   end

   if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
   begin
    raiserror ('Invalid Customer id ',16,1)
    return 2
   end

   if @UnitPrice < 0
   begin
    raiserror ('Unit price can not be negative',16,1)
    return 3
   end

   if @Discount not between 0 and 100
   begin
    raiserror ('Invalid discount',16,1)
    return 4
   end

   if @EndDate < CAST(GETDATE() AS date)
   begin
    raiserror ('Invalid EndDate',16,1)
    return 5
   end

   insert into BookLoans(BookInstanceId, CustomerId, UnitPrice, Discount, EndDate, ConditionId)
   values(@BookInstanceId, @CustomerId, @UnitPrice, @Discount, @EndDate, @ConditionId )

   set @BookLoanId = SCOPE_IDENTITY();
    return 0;

 end
