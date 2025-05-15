using CavePlus.Models;
using DB.Contexts;
using DB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CavePlus.Controllers
{
    public class SeriesController : Controller
    {
        private readonly AppDBContext _context;

        public SeriesController(AppDBContext context)
        {
            _context = context;
        }

        // mostrar el listado de series
        public IActionResult ControlSeries()
        {
            HomeViewModel model = new HomeViewModel
            {
                Series = _context.Series
                .Include(s => s.Productora)
                .Include(s => s.GeneroPrimario)
                .Include(s => s.GeneroSecundario)
                .ToList(),
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList()
            };
            return View(model);
        }


        //CREAR SERIE
        [HttpGet]
        public IActionResult AddSerie()
        {
            var viewModel = new BaseSerieViewModel
            {
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSerie(BaseSerieViewModel viewModel)
        {

            var nuevaSerie = new Series
            {
                Nombre = viewModel.Nombre,
                Portada = viewModel.Portada,
                LinkYT = viewModel.LinkYT,
                ProductoraId = viewModel.ProductoraId,
                GeneroPrimarioId = viewModel.GeneroPrimarioId,
                GeneroSecundarioId = viewModel.GeneroSecundarioId
            };

            await _context.Series.AddAsync(nuevaSerie);
            await _context.SaveChangesAsync();

            return RedirectToAction("ControlSeries");

        }

        [HttpGet]
        public async Task<IActionResult> EditSerie(int id)
        {
            // Buscar la serie por ID
            var serie = await _context.Series
                .Include(s => s.Productora) // Incluir Productora
                .Include(s => s.GeneroPrimario) // Incluir GeneroPrimario
                .Include(s => s.GeneroSecundario) // Incluir GeneroSecundario
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serie == null)
            {
                return NotFound(); // Retornar un NotFound si no se encontró la serie
            }

            // Cargar las listas de productoras y géneros
            var productoras = await _context.Productoras.ToListAsync();
            var generos = await _context.Generos.ToListAsync();

            // Usar BaseSerieViewModel para la edición
            var viewModel = new BaseSerieViewModel
            {
                Id = serie.Id,
                Nombre = serie.Nombre,
                Portada = serie.Portada,
                LinkYT = serie.LinkYT,
                ProductoraId = serie.ProductoraId ?? 0, // Asignar 0 si es nulo
                GeneroPrimarioId = serie.GeneroPrimarioId,
                GeneroSecundarioId = serie.GeneroSecundarioId,
                Productoras = productoras,
                Generos = generos
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSerie(BaseSerieViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Volver a cargar las listas de productoras y géneros
                viewModel.Productoras = await _context.Productoras.ToListAsync();
                viewModel.Generos = await _context.Generos.ToListAsync();
                return View(viewModel);
            }

            // Buscar la serie existente en la base de datos
            var serieExistente = await _context.Series.FindAsync(viewModel.Id);
            if (serieExistente == null)
            {
                return NotFound(); // Retorna NotFound si no se encuentra la serie
            }

            // Actualizar las propiedades de la serie existente
            serieExistente.Nombre = viewModel.Nombre;
            serieExistente.Portada = viewModel.Portada;
            serieExistente.LinkYT = viewModel.LinkYT;
            serieExistente.ProductoraId = viewModel.ProductoraId;
            serieExistente.GeneroPrimarioId = viewModel.GeneroPrimarioId;
            serieExistente.GeneroSecundarioId = viewModel.GeneroSecundarioId;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction("ControlSeries");
        }


        [HttpGet]
        public IActionResult DeleteSerie(int id)
        {
            var serie = _context.Series.Find(id);
            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }

        // Método para eliminar la serie
        [HttpPost, ActionName("DeleteConf")]
        public IActionResult DeleteConf(int id)
        {
            var serie = _context.Series.Include(s => s.SerieGeneros)
                                        .FirstOrDefault(s => s.Id == id);

            if (serie != null)
            {
                _context.Series.Remove(serie);
                _context.SaveChanges();
            }

            return RedirectToAction("ControlSeries"); // Redirige a la lista de series
        }
    }
}
   
