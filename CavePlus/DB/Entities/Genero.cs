using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Genero
    {
        public int GeneroId { get; set; }
        public string Nombre { get; set; }
        public ICollection<SerieGenero> SerieGeneros { get; set; }
    }
}
