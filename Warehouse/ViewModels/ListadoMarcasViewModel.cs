using Warehouse.Models;
using X.PagedList;

namespace Warehouse.ViewModels
{
    public class ListadoMarcasViewModel
    {
        public string SearchTerm { get; set; }

        public int? Pagina { get; set; }

        public IPagedList<Marca> Marcas { get; set; }

        public int Total { get; set; } = 0;
    }
}
