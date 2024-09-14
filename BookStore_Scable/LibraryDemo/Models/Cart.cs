using System.Text.Json.Serialization;

namespace LibraryDemo.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}
