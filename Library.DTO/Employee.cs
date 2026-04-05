using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Employee
{
    [IgnoreForInsert]
    public int EmployeeId { get; set; }
    public int CityId { get; set; }
    public int PositionId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PersonalNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
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
