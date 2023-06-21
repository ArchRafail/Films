using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class FilmCategory
    {
        [Required]
        [Column("film_id")]
        public int FilmId { get; set; }

        public Film? Film { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
