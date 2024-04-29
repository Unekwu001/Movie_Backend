using movieProject.Model;

namespace movieProject.MovieServices
{
    public interface IMovieService
    {
        Task<Movie> MovieSearchAsync(string title);
        Task<IEnumerable<string>> GetSearchHistoryAsync();
    }
}
