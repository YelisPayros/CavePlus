using DB.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CavePlus.Models
{
    public class BaseSerieViewModel 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Portada { get; set; }

        [Required]
        public string LinkYT { get; set; }

        [Required]
        public int? ProductoraId { get; set; }

        [Required]
        public int? GeneroPrimarioId { get; set; }


        public int? SerieId { get; set; } // ID de la serie para el modelo SerieGenero
        public int? GeneroId { get; set; } // ID del género para el modelo SerieGenero
        public int? GeneroSecundarioId { get; set; }

        public List<Productora> Productoras { get; set; } = new List<Productora>();
        public List<Genero> Generos { get; set; } = new List<Genero>();

    }
}