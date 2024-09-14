namespace RestApiServices.Models
{
    public class ApiBook
    {
        public string? Title { get; set; }
        //public List<string>? BookAuthor { get; set; }
        public int? SkipCount { get; set; } = 0;
        public int? MaxResult { get; set; } = 10;
    }
}
