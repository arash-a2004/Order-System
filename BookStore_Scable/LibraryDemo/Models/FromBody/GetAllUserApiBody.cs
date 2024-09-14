namespace LibraryDemo.Models.FromBody
{
    public class GetAllUserApiBody
    {
        public int? SkipCount { get; set; } = 0;
        public int? MaxResult { get; set; } = 10;
        public string? NameFilter { get; set; } = "";
    }
}
