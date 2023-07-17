using GameLibraryApi.Models;

namespace GameLibraryApi.Services
{
    public interface IGameLibRepository
    {
        Task<bool> CreateGame(Game game);
        Task<List<GameDTO>> GetGames();
        Task<List<GameDTO>> GetGamesByGenre(string genre);
        Task<GameDTO> GetGameById(int id);
        Task<bool> UpdateGame(int id, Game game);
        Task<bool> DeleteGame(int id);

        Task CreateGenre(Genre genre);
        Task<List<GenreDTO>> GetGenres();
        Task<bool> DeleteGenre(int id);
    }
}
