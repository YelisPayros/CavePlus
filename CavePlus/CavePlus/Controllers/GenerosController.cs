using Microsoft.AspNetCore.Mvc;
using CavePlus.Models;
using DB.Contexts;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CavePlus.Controllers
{
    public class GenerosController : Controller
    {

        private readonly AppDBContext _context;
        public GenerosController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ControlGeneros()
        {
            var generos = _context.Generos.ToList();
            return View(generos);
        }

        public IActionResult AddGenero()
        {
            return View(new GenerosViewModel());
        }

        [HttpPost]
        public IActionResult AddGenero(GenerosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var genero = new Genero
                {
                    Nombre = viewModel.Nombre
                };
                _context.Generos.Add(genero);
                _context.SaveChanges();
                return RedirectToAction("ControlGeneros");
            }
            return View(viewModel);
        }

        public IActionResult EditGenero(int id)
        {
            var genero = _context.Generos.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            var viewModel = new GenerosViewModel
            {
                GeneroId = genero.GeneroId,
                Nombre = genero.Nombre
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditGenero(GenerosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var genero = _context.Generos.Find(viewModel.GeneroId);
                if (genero == null)
                {
                    return NotFound();
                }
                genero.Nombre = viewModel.Nombre;
                _context.SaveChanges();
                return RedirectToAction("ControlGeneros");
            }
            return View(viewModel);
        }


        public IActionResult DeleteGenero(int id)
        {
            var genero = _context.Generos.Find(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        [HttpPost, ActionName("DeleteGenero")]
        public IActionResult DeleteGeneroConf(int id)
        {
            var genero = _context.Generos.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            // Reemplazar el género en las series asociadas
            var seriesAsociadas = _context.Series.Where(s => s.GeneroPrimarioId == id || s.GeneroSecundarioId == id).ToList();
            foreach (var serie in seriesAsociadas)
            {
                // Si el género que se está eliminando es el primario
                if (serie.GeneroPrimarioId == id)
                {
                    serie.GeneroPrimarioId = null; // Establecer como nulo
                }
                // Si el género que se está eliminando es el secundario
                if (serie.GeneroSecundarioId == id)
                {
                    serie.GeneroSecundarioId = null; // Establecer como nulo
                }
            }

            // Guardar cambios en las series
            _context.SaveChanges();

            // Eliminar el género
            _context.Generos.Remove(genero);
            _context.SaveChanges();

            return RedirectToAction("ControlGeneros");
        }
    }


}

