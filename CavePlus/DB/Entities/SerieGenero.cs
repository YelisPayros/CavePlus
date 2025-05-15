using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class SerieGenero
    {
       public int SerieId { get; set; }
       public Series Series { get; set; }

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        
    }
}
