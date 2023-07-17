using System.Text.Json.Serialization;

namespace GameLibraryApi.Models
{
    public class GameDTO
    {
        public string Name { get; set; }

        public string Developer { get; set; }

        [JsonPropertyName("genres")]
        public List<GenreDTO> GenreDTOs { get; set; }

        public GameDTO(Game game) 
        {
            Name = game.Name;
            Developer = game.Developer;
            GenreDTOs = new List<GenreDTO>();
            foreach (var genre in game.Genres)
            {
                GenreDTOs.Add(new GenreDTO(genre));
            }
        }
    }
}
