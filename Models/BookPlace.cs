using System.ComponentModel.DataAnnotations.Schema;

namespace LibrariumAPI.Models
{
    public class BookPlace
    {   
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ShelfNumber { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }


    }
}
