using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Films.Models
{
    public class FilmActor
    {
        [Required]
        [Column("actor_id")]
        public int ActorId { get; set; }

        public Actor? Actor { get; set; }

        [Required]
        [Column("film_id")]
        public int FilmId { get; set; }

        public Film? Film { get; set; }
    }
}
