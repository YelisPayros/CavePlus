using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Portada { get; set; }
        public string LinkYT { get; set; }
        public int? ProductoraId { get; set; }
        public Productora Productora { get; set; }
        public int? GeneroPrimarioId { get; set; }
        public Genero GeneroPrimario { get; set; }
        public int? GeneroSecundarioId { get; set; }
        public Genero GeneroSecundario { get; set; }
        public ICollection<SerieGenero> SerieGeneros { get; set; }

    }
}
