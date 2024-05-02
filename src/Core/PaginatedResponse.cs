namespace WebApiTemplate.Core;

public class PaginatedResponse<T>
{
    public int Count { get; set; }
    public List<T> Items { get; set; }

    public PaginatedResponse(int count, List<T> items)
    {
        Count = count;
        Items = items;
    }
}
