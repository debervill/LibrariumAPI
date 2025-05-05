namespace LibrariumAPI.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishYear { get; set; }

        public ICollection<BookPlace> BookPlaces { get; set; } = new List<BookPlace>();
        public ICollection<IssueCards> Issues { get; set; }= new List<IssueCards>();
    }
}
