// FILE : Publisher.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Publishers for the games within the store

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MistAPI.Models.Entities
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
        [JsonIgnore]
        public List<GameInApp> GamesInApp { get; set; } = new();
    }
}
