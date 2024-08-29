using System.Text.Json.Serialization;

namespace RestApiServices.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public List<Author> Authors { get; set; }
        [JsonIgnore]
        public List<Cart> Carts { get; set; }
    }
}
