using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TFTDataTrackerApi.Models
{
    public class Sets
    {
        public int id { get; set; }
        [Required]
        public int SetNumber { get; set; }

    }
}
