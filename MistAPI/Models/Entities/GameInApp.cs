// FILE : GameInApp.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// games within the application and store

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MistAPI.Models.Entities
{
    public class GameInApp
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
        public Publisher? Publisher { get; set; }
    }
}
