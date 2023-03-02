namespace AirBooker.Domain.Models.Responses
{
    public class DataResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
