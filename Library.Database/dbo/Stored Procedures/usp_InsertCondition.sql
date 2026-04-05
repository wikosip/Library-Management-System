create procedure usp_InsertCondition
	@Name nvarchar(20),
	@Value tinyint,
    @ConditionId int output
as
begin
	set nocount on;

	if LEN(@Name) < 3
	begin
	raiserror('Condition name must be at least  characters long.', 16, 1, @ConditionId);
	return 1;
	end

	if exists (select 1 from Conditions where Name = @Name and IsDeleted = 0)
	begin
	raiserror('Condition name already exists', 16, 3)
	return 3;
	end

	insert into Conditions (Name, Value)
	values (@Name, @Value);
	set @ConditionId = scope_identity();

	return 0;
end