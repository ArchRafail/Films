using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Films.Models
{
    public class Language
    {
        [Key]
        [Column("language_id")]
        public int LanguageId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set;}

        [Required]
        [Column("last_update")]
        public DateTime LastUpdate { get; set;}

        public ICollection<Film>? Films { get; set;}
        public ICollection<Film>? OriginalFilms { get; set;}
    }
}
