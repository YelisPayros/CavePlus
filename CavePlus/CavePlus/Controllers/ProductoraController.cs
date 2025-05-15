using DB.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DB.Entities;
using CavePlus.Models;

namespace CavePlus.Controllers
{
    public class ProductoraController : Controller
    {
        private readonly AppDBContext _context;
        public ProductoraController(AppDBContext context)
        {
            _context = context;
        }


        private static List<Productora> _productoras = new List<Productora>();

        // Acción para listar todas las productoras
        public IActionResult ControlProductora()
        {

            var productoras = _context.Productoras.Select(p => new ProductoraViewModel
            {
                ProductoraId = p.ProductoraId,
                Nombre = p.Nombre
            }).ToList();

            var model = new ProductoraListViewModel
            {
                Productoras = productoras
            };

            return View(model);
        }


        //CREAR PRODUCTORA

        // GET: Productora/Crear
        public IActionResult AddProductora()
        {
            return View(new ProductoraViewModel());
        }

        // POST: Productora/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddConf(ProductoraViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevaProductora = new Productora
                {
                    Nombre = model.Nombre
                };

                _context.Productoras.Add(nuevaProductora);
                _context.SaveChanges();

                return RedirectToAction("ControlProductora");
            }

            return View("AddProductora", model);
        }

        // Editar productora
        // GET: Productora/Edit/5
        public IActionResult EditProductora(int id)
        {
            var productora = _context.Productoras.Find(id);
            if (productora == null)
            {
                return NotFound();
            }

            var model = new ProductoraViewModel
            {
                ProductoraId = productora.ProductoraId,
                Nombre = productora.Nombre
            };

            return View(model);
        }

        // POST: Productora/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditConf(ProductoraViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productora = _context.Productoras.Find(model.ProductoraId);
                if (productora == null)
                {
                    return NotFound();
                }

                productora.Nombre = model.Nombre;

                _context.SaveChanges();

                return RedirectToAction("ControlProductora");
            }

            return View("EditProductora", model);
        }

        // GET: Productora/Eliminar/5
        public IActionResult DeleteProductora(int id)
        {
            var productora = _context.Productoras.Find(id);
            if (productora == null)
            {
                return NotFound(); // Maneja el caso donde la productora no existe
            }

            // Crear el ViewModel y mapear los datos de la productora
            var productoraViewModel = new ProductoraViewModel
            {
                ProductoraId = productora.ProductoraId,
                Nombre = productora.Nombre
                // Agrega otras propiedades si es necesario
            };

            return View(productoraViewModel); // Muestra la vista de confirmación de eliminación
        }

        // POST: Productora/Eliminar/5
        [HttpPost, ActionName("DeleteProductora")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePConf(int id)
        {
            var productora = _context.Productoras.Find(id);
            if (productora == null)
            {
                return NotFound(); // Maneja el caso donde la productora no existe
            }

            // Cambiar la ProductoraId de las series asociadas a "Sin productora"
            var sinProductora = _context.Productoras.FirstOrDefault(p => p.Nombre == "Sin productora");

            if (sinProductora != null)
            {
                // Obtener todas las series asociadas a la productora que se va a eliminar
                var seriesAsociadas = _context.Series.Where(s => s.ProductoraId == productora.ProductoraId).ToList();
                foreach (var serie in seriesAsociadas)
                {
                    serie.ProductoraId = sinProductora.ProductoraId; // Asigna a "Sin productora"
                }

                _context.Series.UpdateRange(seriesAsociadas); // Actualiza las series
            }

            _context.Productoras.Remove(productora); // Elimina la productora
            _context.SaveChanges();

            return RedirectToAction("ControlProductora"); // Redirige al listado de productoras
        }
    }
}
