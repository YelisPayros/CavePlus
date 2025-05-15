using CavePlus.Models;
using DB.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace CavePlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, int? selectProductoraId, List<int> selectGeneroId)
        {
            // Redirigir a la vista de resultados de búsqueda si hay filtros aplicados
            if (!string.IsNullOrEmpty(searchString) || selectProductoraId.HasValue || (selectGeneroId != null && selectGeneroId.Count > 0))
            {
                return RedirectToAction("SearchResult", new { searchString, selectProductoraId, selectGeneroId });
            }

            // Si no hay filtros, simplemente muestra la vista de inicio
            var viewModel = new HomeViewModel
            {
                Series = _context.Series.Include(s => s.Productora).ToList(),
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList()
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SearchResult(string searchString, int? selectProductoraId, List<int> selectGeneroId)
        {
            if (string.IsNullOrEmpty(searchString) && !selectProductoraId.HasValue && (selectGeneroId == null || selectGeneroId.Count == 0))
            {
                // Si no hay filtros ni búsqueda, redirigir a la vista de inicio (Index)
                return RedirectToAction("Index");
            }
            var query = _context.Series
                .Include(s => s.Productora)
                .Include(s => s.GeneroPrimario)
                .Include(s => s.GeneroSecundario)
                .AsQueryable();

            // Aplica filtros
            query = ProductoraFilter(query, selectProductoraId);
            query = GeneroFilter(query, selectGeneroId);
            query = SearchFilter(query, searchString);

            var viewModel = new HomeViewModel
            {
                Series = query.ToList(),
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList(),
                actFilter = searchString,
                selectProductoraId = selectProductoraId,
                selectGeneroId = selectGeneroId
            };

            // Verificar si hay resultados
            if (!viewModel.Series.Any())
            {
                ViewData["Message"] = "No se encontraron resultados.";
            }

            return View(viewModel);
        }

        private IQueryable<Series> ProductoraFilter(IQueryable<Series> query, int? selectProductoraId)
        {
            if (selectProductoraId.HasValue)
            {
                query = query.Where(s => s.ProductoraId == selectProductoraId.Value);
            }
            return query;
        }

        private IQueryable<Series> GeneroFilter(IQueryable<Series> query, List<int> selectGeneroId)
        {
            if (selectGeneroId != null && selectGeneroId.Count > 0)
            {
                query = query.Where(s =>
                    (s.GeneroPrimarioId.HasValue && selectGeneroId.Contains(s.GeneroPrimarioId.Value)) ||
                    (s.GeneroSecundarioId.HasValue && selectGeneroId.Contains(s.GeneroSecundarioId.Value))
                );
            }
            return query;
        }

        private IQueryable<Series> SearchFilter(IQueryable<Series> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                query = query.Where(s => EF.Functions.Like(s.Nombre.ToLower(), $"%{lowerSearchString}%"));
            }
            return query;
        }

        public async Task<IActionResult> Details(int id)
        {
            var serie = await _context.Series
                .Include(s => s.Productora)
                .Include(s => s.GeneroPrimario)
                .Include(s => s.GeneroSecundario)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serie == null)
            {
                return NotFound(); // Manejo de la serie no encontrada
            }

            var viewModel = new DetailsViewModel
            {
                Id = serie.Id,
                Nombre = serie.Nombre,
                Portada = serie.Portada,
                LinkYT = serie.LinkYT,
                Productora = serie.Productora?.Nombre ?? "No disponible", // Manejo de productora nula
                GeneroPrimario = serie.GeneroPrimario?.Nombre ?? "No definido", // Manejo de género primario nulo
                GeneroSecundario = serie.GeneroSecundario?.Nombre // Puede ser nulo, se maneja en la vista
            };

            return View(viewModel);
        }

    }
}





