namespace GameLibraryApi.Models
{
    public class GenreDTO
    {
        public string Name { get; set; }

        public GenreDTO(Genre genre)
        {
            Name = genre.Name;
        }
    }
}
