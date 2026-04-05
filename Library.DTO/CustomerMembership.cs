using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class CustomerMembership
{
    [IgnoreForInsert]
    public int CustomerMembershipId { get; set; }
	public int CustomerId { get; set; }
	public int MembershipTypeId { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public bool IsDeleted { get; set; }
}
