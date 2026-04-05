create procedure usp_AssignPermissionToRole
    @RoleId int,
    @PermissionId int
as
begin
    set nocount on;

    if not exists (select 1 from Roles where RoleId = @RoleId)
    begin
        raiserror('RoleId %d does not exist.', 16, 1, @RoleId);
        return 1;
    end

    if exists (select 1 from Roles where RoleId = @RoleId and IsDeleted = 1)
    begin
        raiserror('RoleId %d is deleted.', 16, 1, @RoleId);
        return 1;
    end

    if not exists (select 1 from Permissions where PermissionId = @PermissionId)
    begin
        raiserror('PermissionId %d does not exist.', 16, 1, @PermissionId);
        return 1;
    end

    if exists (select 1 from Permissions where PermissionId = @PermissionId and IsDeleted = 1 )
    begin
        raiserror('PermissionId %d is deleted.', 16, 1, @PermissionId);
        return 1;
    end

    if exists (
        select 1
        from RolePermissions
        where RoleId = @RoleId
          and PermissionId = @PermissionId
    )
    begin
        raiserror('Permission is already assigned to this role.', 16, 1);
        return 1;
    end

    insert into RolePermissions (RoleId, PermissionId)
    values (@RoleId, @PermissionId);

    return 0;
end