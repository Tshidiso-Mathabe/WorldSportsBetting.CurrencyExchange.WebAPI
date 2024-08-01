namespace WorldSportsBetting.CurrencyExchange.Domain.ViewModels
{
    public class PaginatedListViewModel<T>
    {
        public PaginatedListViewModel(T[] items, int pageIndex, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }

        public T[] Items { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
