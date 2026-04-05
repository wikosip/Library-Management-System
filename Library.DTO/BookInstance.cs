using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class BookInstance
{
	[IgnoreForInsert]
	public int BookInstanceId { get; set; }
	public int BookId { get; set; }
	public int ConditionId { get; set; }
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
