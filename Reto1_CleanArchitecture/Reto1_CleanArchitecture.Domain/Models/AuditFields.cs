namespace Reto1_CleanArchitecture.Domain.Models
{
    public class AuditFields
    {
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public string? StatusChangedBy { get; set; }
        public string? StatusChangedDate { get; set; }
    }
}
