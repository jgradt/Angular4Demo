using System.Collections.Generic;

namespace WebApiDemo.Infrastructure
{
    public class PagedData<TData>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<TData> Items { get; set; }
    }
}
