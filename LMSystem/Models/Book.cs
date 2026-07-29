using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public bool IsBorrowed { get; set; } = false;

        public string? BorrowedByUserId { get; set; }
        public string? BorrowedByEmail { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}