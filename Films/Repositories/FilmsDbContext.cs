using Films.Models;
using Microsoft.EntityFrameworkCore;

namespace Films.Repositories
{
    public class FilmsDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmActor> FilmsActors { get; set; }
        public DbSet<FilmCategory> FilmsCategories { get; set; }

        public FilmsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().ToTable("actor", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Language>().ToTable("language", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Category>().ToTable("category", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Film>().ToTable("film", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<FilmActor>().ToTable("film_actor", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<FilmCategory>().ToTable("film_categoryr", t => t.ExcludeFromMigrations());

            modelBuilder.Entity<Film>()
                .HasOne(f => f.Language)
                .WithMany(l => l.Films)
                .HasForeignKey(f => f.LanguageId);

            modelBuilder.Entity<Film>()
                .HasOne(f => f.OriginalLanguage)
                .WithMany(l => l.OriginalFilms)
                .HasForeignKey(f => f.OriginalLanguageId);

            modelBuilder.Entity<Film>()
                .HasMany(f => f.Actors)
                .WithMany(a => a.Films)
                .UsingEntity<FilmActor>(x => x
                    .ToTable("film_actor")
                );

            modelBuilder.Entity<Film>()
                .HasMany(f => f.Categories)
                .WithMany(c => c.Films)
                .UsingEntity<FilmCategory>(x => x
                    .ToTable("film_category")
                );
        }
    }
}
