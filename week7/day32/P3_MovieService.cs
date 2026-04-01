using WebApplication6.Models;
using WebApplication6.Repositories;
namespace WebApplication6.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)    
        {
            _repository = repository;
        }

       public  IEnumerable<Movie> GetMovies()
        {
            return _repository.GetAll();
        }
       public  Movie GetMovie(int id)
        {
            return _repository.GetById(id);
        }
        public void CreateMovie(Movie movie)
        {
             _repository.Add(movie);  
        }
       public  void UpdateMovie(Movie movie)
        {
            _repository.Update(movie);
        }
       public void DeleteMovie(int id)
        {
            _repository.Delete(id);
        }
    }
}
