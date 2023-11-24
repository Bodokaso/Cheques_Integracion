using Cheques_Integracion.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheques_Integracion.Controllers
{
    public class ConceptosPagoController : Controller
    {
        private readonly AppDbContext dbContext;

        public ConceptosPagoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.ConceptosPagos.OrderByDescending(x => x.Id).ToList());
        }

        [HttpGet]
        [Route("ConceptosPago/Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Route("ConceptosPago/{parametro}")]
        public IActionResult Search(string parametro = null)
        {
            if (parametro == "Search")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View("Index", dbContext.ConceptosPagos.Where(x => (x.Id.ToString() == parametro
                    || x.Descripcion.ToLower().Contains(parametro.ToLower())
                    || (x.Estado && (parametro.ToLower() == "activo"))
                    || (!x.Estado && (parametro.ToLower() == "inactivo")))).OrderByDescending(x => x.Id).ToList());
            }

        }

        [HttpGet]
        [Route("ConceptosPago/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            return View("Edit", dbContext.ConceptosPagos.Find(id));
        }

        [HttpPost]
        public IActionResult Add(ConceptosPago conceptospago)
        {
            if (!ModelState.IsValid)
            {
                TempData["ConceptosPago"] = "Formulario invalido, por favor revisar los campos!";
                return View();
            }

            dbContext.Add(conceptospago);
            dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [Route("ConceptosPago/Edit")]
        public IActionResult Edit(ConceptosPago conceptospago)
        {
             dbContext.Update(conceptospago);
             dbContext.SaveChanges();
             return RedirectToAction("Index");
            
        }

        [HttpGet]
        [Route("ConceptosPago/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.ConceptosPagos.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
