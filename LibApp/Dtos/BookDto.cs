namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberInStock { get; set; }
    }
}
