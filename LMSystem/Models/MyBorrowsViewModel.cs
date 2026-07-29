namespace LMSystem.Models
{
    public class MyBorrowsViewModel
    {
        public List<Book> Books { get; set; } = new();
        public List<Magazine> Magazines { get; set; } = new();
        public List<Newspaper> Newspapers { get; set; } = new();
    }
}