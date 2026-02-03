namespace Practice_WebAPICRUD.Models
{
    public class RequestModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? student_name { get; set; }

        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
