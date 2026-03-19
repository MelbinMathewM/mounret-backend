public class EnquiryDto
{
    public int Id { get; set; }   // ✅ IMPORTANT

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Country { get; set; }
    public string UserType { get; set; }
    public int ProductId { get; set; }

    public DateTime CreatedAt { get; set; }  // ✅ useful for admin
}