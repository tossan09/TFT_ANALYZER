using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace TFTDataTrackerApi.Models
{
    public class Comps
    {
        public int Id {  get; set; }
        [Required] 
        public string Name { get; set; } = string.Empty;
        
        public string? Traits { get; set; } = string.Empty; // nova tabela traits?
        
        public string? Style { get; set; } = string.Empty; //trocar tipagem certa
        [Required]
        public int SetId { get; set; }
    }
}
