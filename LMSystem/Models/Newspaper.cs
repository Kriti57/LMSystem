namespace LMSystem.Models
{
    public class Newspaper
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsBorrowed { get; set; } = false;

        public string? BorrowedByUserId { get; set; }
        public string? BorrowedByEmail { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}