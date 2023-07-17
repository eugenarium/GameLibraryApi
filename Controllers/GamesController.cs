using GameLibraryApi.Models;
using GameLibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGameLibRepository _context;

        public GamesController(IGameLibRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame(Game game)
        {
            var created = await _context.CreateGame(game);
            return created?CreatedAtAction(nameof(GetGameById), new { id = game.Id }, new GameDTO(game)):NotFound();
        }

        [HttpGet]       
        public async Task<ActionResult<List<GameDTO>>> GetGames()
        {
            var games = await _context.GetGames();
            return games.Count > 0 ? Ok(games) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGameById(int id)
        {
            var game = await _context.GetGameById(id);
            return game != null ? Ok(game) : NotFound();
        }

        [HttpGet]
        [Route("bygenre")]
        public async Task<ActionResult<List<Game>>> GetGamesByGenre(string genre)
        {
            var games = await _context.GetGamesByGenre(genre);
            return games.Count > 0 ? Ok(games) : NotFound();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, Game game)
        {
            var updated = await _context.UpdateGame(id, game);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var deleted = await _context.DeleteGame(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
