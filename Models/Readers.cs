namespace LibrariumAPI.Models
{
    public class Readers
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        public ICollection<IssueCards> Issues { get; set; } =  new List<IssueCards>();


    }
}
