using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Payment
{
    [IgnoreForInsert]
    public int PaymentId { get; set; }
	public int CustomerId { get; set; }
	public int EmployeeId { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime PaymentDate { get; set; }
	public decimal Amount { get; set; }
}
