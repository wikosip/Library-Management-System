create procedure usp_DeleteCondition
	@ConditionId int
as
begin
	set nocount on;

	if not exists (select 1 from Conditions where ConditionId = @ConditionId and IsDeleted = 0)
	begin
	raiserror('ConditionId does not exist.', 16, 1, @ConditionId);
	return 1;
	end

	update Conditions
	set IsDeleted = 1,
		UpdateDate = getdate()
	where ConditionId = @ConditionId ;

	return 0;
end