using Films.Interfaces;
using Films.Models;
using Microsoft.EntityFrameworkCore;

namespace Films.Repositories
{
    public class FilmsRepositories : IFilmsRepository
    {
        private readonly FilmsDbContext context;

        public FilmsRepositories(FilmsDbContext context)
        {
            this.context = context;
        }

        public List<Film> GetAll()
        {
            return context.Films.Include(x => x.Language).Include(x => x.Actors).Include(x => x.Categories).ToList();
        }
        
    }
}
