using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MistAPI.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        [Required]
        [MaxLength(100)]
        public string PublisherName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3,1)")]
        public decimal? PublisherRating { get; set; }

        // 🔗 Relationship
        public List<GamesInApp> Games { get; set; } = new();
    }
}
