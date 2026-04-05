create procedure usp_DeleteMembershipType
	@MembershipTypeId int
as
begin
	set nocount on;

	if not exists (select 1 from MembershipTypes where MembershipTypeId = @MembershipTypeId and IsDeleted = 0)
	begin
		raiserror('MembershipTypeId %d does not exist.', 16, 1, @MembershipTypeId);
		return 1;
	end

	update MembershipTypes
	set IsDeleted = 1,
		UpdateDate = getdate()
	where MembershipTypeId = @MembershipTypeId;

	return 0;
end