create procedure usp_DeletePublisher
    @PublisherId int
as
begin
    set nocount on;

    if not exists (select 1 from Publishers where PublisherId = @PublisherId and IsDeleted = 0)
    begin
        raiserror('PublisherId does not exist.', 16, 1);
        return 1;
    end

    update Publishers
    set IsDeleted = 1,
        UpdateDate = getdate()
    where PublisherId = @PublisherId;

    return 0;
end