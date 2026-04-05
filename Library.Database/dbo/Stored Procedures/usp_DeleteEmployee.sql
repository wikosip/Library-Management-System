create procedure usp_DeleteEmployee
    @EmployeeId int
as
begin
    set nocount on;

    if not exists (select 1 from Employees where EmployeeId = @EmployeeId)
    begin
        raiserror('EmployeeId %d does not exist in the database.', 16, 1, @EmployeeId);
        return 1;
    end

    if exists (select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 1)
    begin
        raiserror('EmployeeId %d is already deleted.', 16, 1, @EmployeeId);
        return 2;
    end

    update Employees
    set IsDeleted = 1,
        UpdateDate = getdate()
    where EmployeeId = @EmployeeId;

    return 0;
end