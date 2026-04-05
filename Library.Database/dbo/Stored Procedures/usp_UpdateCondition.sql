create procedure usp_UpdateCondition
	@ConditionId int,
	@Name nvarchar(20),
	@Value tinyint
	
as
begin
	set nocount on;

	if @Name is null or LEN(@Name) < 3
	begin
	raiserror('Condition name must be at least  characters long.', 16, 1, @ConditionId);
	return 1;
	end

	if exists (select 1 from Conditions where Name = @Name and ConditionId != @ConditionId and IsDeleted = 0)
	begin
	raiserror('Condition name already exists', 16, 3)
	return 2;
	end

	if not exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 0)
	begin
	raiserror('ConditionId does not exist.', 16, 1, @ConditionId);
	return 3;
	end

	update Conditions
	set Name = @Name,
		Value = @Value,
		UpdateDate = getdate()
	where ConditionId = @ConditionId and 
		  (Name != @Name or 
		  Value != @Value );

	return 0;
end