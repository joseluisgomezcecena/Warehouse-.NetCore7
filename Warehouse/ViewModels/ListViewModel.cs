using Warehouse.Models;
using X.PagedList;

namespace Warehouse.ViewModels
{
    public class ListViewModel<T>
    {
        public string SearchTerm { get; set; }
        public int? Pagina { get; set; }
        public IPagedList<T> Records { get; set; }
        public int Total { get; set; } = 0;

    }
}
