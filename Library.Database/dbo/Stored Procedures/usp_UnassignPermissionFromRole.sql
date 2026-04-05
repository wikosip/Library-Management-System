create procedure usp_UnassignPermissionFromRole
    @RoleId int,
    @PermissionId int
as
begin
    set nocount on;

    if not exists (
      select 1
      from RolePermissions
      where RoleId = @RoleId
      and PermissionId = @PermissionId
    )
    begin
        raiserror('Permission %d is not assigned to Role %d.', 16, 1);
        return 1;
    end

    delete from RolePermissions
    where RoleId = @RoleId
      and PermissionId = @PermissionId;

    return 0;
end