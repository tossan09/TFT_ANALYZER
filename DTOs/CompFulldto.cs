using System.ComponentModel.DataAnnotations;

namespace TFTDataTrackerApi.DTOs
{
    public class CompDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Traits { get; set; }
        public string? Style { get; set; }
        [Required]
        public int SetNumber { get; set; }
    }
}
