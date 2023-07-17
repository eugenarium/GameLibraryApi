using GameLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Services
{
    public class GameLibRepository : IGameLibRepository
    {
        private readonly LibDb _dbContext;

        public GameLibRepository(LibDb dbContext)
        {
            _dbContext = dbContext;
        }


        //работа с играми
        public async Task<bool> CreateGame(Game creatingName)
        {
            var game = new Game { Name = creatingName.Name, Developer = creatingName.Developer, Genres = new List<Genre>() };
            foreach (var genre in creatingName.Genres)
            {
                var g = await _dbContext.Genres
                    .FirstOrDefaultAsync(gen => gen.Name.Equals(genre.Name));

                //if (g == null)
                //{
                //    await CreateGenre(genre);
                //    g = genre;
                //}
                if (g == null) return false;

                game.Genres.Add(g);
                if (g.Games == null) g.Games = new List<Game>();
                g.Games.Add(game);
            }

            _dbContext.Games.Add(game);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<GameDTO> GetGameById(int id)
        {
            var game = await _dbContext.Games.Include(game => game.Genres)
                .FirstOrDefaultAsync(game => game.Id == id);
            return game != null ? new GameDTO(game) : null;
        }

        public async Task<List<GameDTO>> GetGames()
        {
            return await _dbContext.Games.Include(game=>game.Genres)
                .Select(game => new GameDTO(game))
                .ToListAsync();
        }

        public async Task<List<GameDTO>> GetGamesByGenre(string genre)
        {
            return await _dbContext.Games.Include(game=>game.Genres)
                .Where(game => game.Genres.Any(gen => gen.Name == genre))
                .Select(game => new GameDTO(game))
                .ToListAsync();
        }

        public async Task<bool> UpdateGame(int id, Game gameChanged)
        {
            var game = _dbContext.Games.Include(game => game.Genres)
                .FirstOrDefault(game => game.Id == id);

            if (game == null) return false;

            game.Name = gameChanged.Name;
            game.Developer = gameChanged.Developer;

            var genresChanged = new List<Genre>();
            foreach (var genre in gameChanged.Genres) 
            {
                var g = await _dbContext.Genres
                    .FirstOrDefaultAsync(gen => gen.Name.Equals(genre.Name));

                //if (g == null)
                //{
                //    await CreateGenre(genre);
                //    g = genre;
                //}
                if (g == null)
                    return false;

                genresChanged.Add(g);
            }

            game.Genres = genresChanged;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGame(int id)
        {
            var game = await _dbContext.Games.FindAsync(id);

            if (game == null) return false;

            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
            return true;
        }



        //Работа с жанрами

        public async Task<List<GenreDTO>> GetGenres()
        {
            return await _dbContext.Genres
                .Select(g=>new GenreDTO(g))
                .ToListAsync();
        }

        public async Task CreateGenre(Genre genre)
        {
            
            var existingGenre = await _dbContext.Genres.FirstOrDefaultAsync(gen => gen.Name.Equals(genre.Name));
            if (existingGenre != null) { return; }

            genre.Games = new List<Game>();
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> UpdateGenre(int id, Genre genre)
        {
            var gen = await _dbContext.Genres.FindAsync(id);

            if (gen is null) return false;

            gen.Name = genre.Name;
            gen.Games = genre.Games;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);

            if (genre is null) return false;

            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckIfGenreExists(string name)
        {
            return await _dbContext.Genres
                .AnyAsync(genre => genre.Name.Equals(name));
        }

    }
}
