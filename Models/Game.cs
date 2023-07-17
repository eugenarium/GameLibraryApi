using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Developer { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
