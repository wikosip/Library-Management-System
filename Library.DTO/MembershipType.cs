using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class MembershipType
{
    [IgnoreForInsert]
    public int MembershipTypeId { get; set; }
	public string Name { get; set; } = null!;
	public short DurationDay { get; set; }
	public decimal Price { get; set; }
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
