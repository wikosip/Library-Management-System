using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class City
{
    [IgnoreForInsert]
    public int CityId { get; set; }
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string IsoName { get; set; } = null!;
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