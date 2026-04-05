using Library.DTO.Attributes;

namespace Library.DTO;

public sealed class Book
{
    [IgnoreForInsert]
    public int BookId { get; set; }
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
    public string Title { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public short PublicationYear { get; set; }
    public short PageCount { get; set; }
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
