namespace Studentby.Shared.Structures;

public class PaginatedList<T>
{
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }

    public IEnumerable<T> Data { get; set; }

    public PaginatedList(IEnumerable<T> data, int totalItems, int pageSize, int currentPage, int totalPages)
    {
        Data = data;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalItems = totalItems;
        CurrentPage = currentPage;
    }

    public PaginatedList<U> TransformDataType<U>(IEnumerable<U> data)
    {
        return new PaginatedList<U>(data, TotalItems, PageSize, CurrentPage, TotalPages);
    }
}
