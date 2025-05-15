using DB.Entities;

namespace CavePlus.Models
{
    public class HomeViewModel
    {
        public List<Series> Series { get; set; }
        public List<Productora> Productoras { get; set; } = new List<Productora>();
        public List<Genero> Generos { get; set; } = new List<Genero>();



        //filtros
        public int? selectProductoraId { get; set; }
        
        public List<int> selectGeneroId { get; set; } = new List<int>();
        public string actFilter { get; set; }

    }

    /* public static implicit operator HomeViewModel(HomeViewModel v)
     {
         throw new NotImplementedException();
     }*/
}

