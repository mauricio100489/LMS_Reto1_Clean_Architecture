namespace Reto1_CleanArchitecture.Domain.Models
{
    public class Contacts : AuditFields
    {
        public long ContactId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public long Status { get; set; }
    }
}
