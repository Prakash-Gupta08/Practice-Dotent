namespace Practice_WebAPICRUD.PaginationResultRequest
{
    public class PageResult<T>
    {
        public int  PageNumber { get; set; }
        public int PageSize {  get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public List<T> Data { get; set; }
    }
}
