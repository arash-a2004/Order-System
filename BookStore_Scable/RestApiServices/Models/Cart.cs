namespace RestApiServices.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}
