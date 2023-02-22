namespace AirBooker.Domain.Models.Responses
{
    public class PaginatedDataResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
