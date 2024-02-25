using Humanizer;
using System.ComponentModel.DataAnnotations;
namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the book's title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the book's author.")]
        public string Author { get; set; }
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Please select the book's genre.")]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "Please enter the book's release date.")]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please enter the book's stock.")]
        [Range(1, 20, ErrorMessage = "The field Number in Stock must be between 1 and 20.")]
        public int NumberInStock { get; set; }
        
    }
}
