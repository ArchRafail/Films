using Films.Models;

namespace Films.Interfaces
{
    public interface IFilmsRepository
    {
        public List<Film> GetAll();
       
    }
}
