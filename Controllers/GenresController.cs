using GameLibraryApi.Models;
using GameLibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : Controller
    {
        private readonly IGameLibRepository _context;

        public GenresController(IGameLibRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres()
        {
            return await _context.GetGenres();
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGenre(Genre genre)
        {
            await _context.CreateGenre(genre);
            return CreatedAtAction(nameof(GetGenres), new { id = genre.Id }, genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var x = await _context.DeleteGenre(id);
            return x ? NoContent() : NotFound();
        }
    }
}
