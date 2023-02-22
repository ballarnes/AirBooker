namespace AirBooker.Data.Entities
{
    public class PaginatedData<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; } = null!;
    }
}
