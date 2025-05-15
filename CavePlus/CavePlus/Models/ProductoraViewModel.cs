using DB.Contexts;
using DB.Entities;

namespace CavePlus.Models
{
    public class ProductoraViewModel 
    {
        public int ProductoraId { get; set; }

        public string Nombre { get; set; }
    }

    public class ProductoraListViewModel 
    {
        public List<ProductoraViewModel> Productoras { get; set; } // Listado de productoras

    }
}
