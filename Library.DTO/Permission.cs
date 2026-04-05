using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Permission
{
    [IgnoreForInsert]
    public int PermissionId { get; set; }
	public string Name { get; set; } = null!;
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public bool IsDeleted { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime CreateDate { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? UpdateDate { get; set; }
}
