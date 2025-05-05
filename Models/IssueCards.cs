using System.ComponentModel.DataAnnotations.Schema;

namespace LibrariumAPI.Models
{
    public class IssueCards
    {

        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }


        [ForeignKey("Librarian")]
        public int LibrarianId { get; set; }

    }
}
