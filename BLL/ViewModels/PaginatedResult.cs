namespace BLL.ViewModels
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public required List<T> Data { get; set; }
    }
}
