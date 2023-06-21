using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Films.Models
{
    public class Film
    {
        [Key]
        [Column("film_id")]
        public int FilmId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set;}

        [Column("description")]
        public string? Description { get; set;}

        [Column("release_year")]
        public string? ReleaseYear { get; set;}

        [Required]
        [Column("language_id")]
        public int LanguageId { get; set;}
        public Language Language { get; set;}

        [Column("original_language_id")]
        public int? OriginalLanguageId { get; set;}
        public Language? OriginalLanguage { get; set;}

        [Required]
        [Column("rental_duration")]
        public byte RentalDuration { get; set;}

        [Required]
        [Column("rental_rate")]
        public decimal RentalRate { get; set;}

        [Column("length")]
        public Int16? Length { get; set;}

        [Required]
        [Column("replacement_cost")]
        public decimal ReplacementCost { get; set;}

        [Column("rating")]
        public string? Rating { get; set;}

        [Column("special_features")]
        public string? SpecialFeatures { get; set; }

        [Required]
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

        public ICollection<Actor>? Actors { get; set;}
        public ICollection<Category>? Categories { get; set;}
    }
}
