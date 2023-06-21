using Films.Interfaces;
using Films.Models;

namespace Films.Services
{
    public class FilmsServices : IFilmsService
    {
        private readonly IFilmsRepository filmsRepository;

        public FilmsServices(IFilmsRepository filmsRepository)
        {
            this.filmsRepository = filmsRepository;
        }

        public List<Film> GetAll()
        {
            return filmsRepository.GetAll();
        }
    }
}
