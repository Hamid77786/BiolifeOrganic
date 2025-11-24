namespace BiolifeOrganic.Dll.DataContext.Entities
{
    public class Contact : TimeStample
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsDefault { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
