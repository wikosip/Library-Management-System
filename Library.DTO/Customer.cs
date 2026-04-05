using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Customer
{
    [IgnoreForInsert]
    public int CustomerId { get; set; }
    public int CityId { get; set; }
    public string PersonalNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; } 
    public string? PhoneNumber { get; set; }
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
