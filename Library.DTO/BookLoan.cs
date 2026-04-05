using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class BookLoan
{
    [IgnoreForInsert]
    public int BookLoanId { get; set; }
    public int BookInstanceId { get; set; }
    public int ConditionId { get; set; }
    public int CustomerId { get; set; }
    public decimal UnitPrice { get; set; }
    public float? Discount { get; set; } 
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime StartDate { get; set; }
    [IgnoreForUpdate]
    public DateTime EndDate { get; set; }
    [IgnoreForInsert]
    public DateTime? RealEndDate { get; set; }
    [IgnoreForInsert]
    public decimal? FineAmount { get; set; }
    [IgnoreForInsert]
    public bool IsReturned { get; set; }
}
