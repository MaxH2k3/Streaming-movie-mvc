using System.Collections;

namespace SMovie.Domain.Models
{
    public class PagedList<T> : IEnumerable<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int EachPage { get; set; }

        public PagedList(IEnumerable<T> items, int totalItems, int currentPage, int eachPage)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            EachPage = eachPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)eachPage);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
