using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Condition
{
    [IgnoreForInsert]
    public int ConditionId { get; set; }
    public string Name { get; set; } = null!;
    public byte Value { get; set; }
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
