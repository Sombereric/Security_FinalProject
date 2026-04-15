using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MistAPI.Models
{
    public class GamesInApp
    {
        [Key]
        public int GameID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? GameName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal GamePrice { get; set; }

        [Required]
        public int PublisherID { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal? GameRating { get; set; }

        [MaxLength(50)]
        public string? GameGenre { get; set; }

        [ForeignKey("PublisherID")]
        public Publishers? Publisher { get; set; }
    }
}
