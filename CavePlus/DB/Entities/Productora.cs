using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Productora
    {
        public int ProductoraId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Series> Series { get; set; }
    }
}
