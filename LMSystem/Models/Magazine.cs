namespace LMSystem.Models
{
    public class Magazine
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; } = false;

        public string? BorrowedByUserId { get; set; }
        public string? BorrowedByEmail { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}