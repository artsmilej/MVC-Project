using System.ComponentModel.DataAnnotations;

namespace LibraryBookApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Range(1500, 2050)]
        public int Year { get; set; }

        public string Publisher { get; set; }
    }
}