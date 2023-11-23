using Cheques_Integracion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheques_Integracion.Controllers
{
    public class RegistroSolicitudChequeController : Controller
    {
        private readonly AppDbContext dbContext;

        public RegistroSolicitudChequeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {

            return View(dbContext.RegistroSolicitudCheques.OrderByDescending(x => x.Id).ToList());
        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/Add")]
        public IActionResult Add()
        {
            FillDropdownLists();
            return View();
        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/{parametro}")]
        public IActionResult Search(string parametro = null)
        {
            if (parametro == "Search")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View("Index", dbContext.RegistroSolicitudCheques.Where(x => (x.Id.ToString() == parametro
                    
                    || x.NumeroSolicitud.ToLower().Contains(parametro.ToLower())
                    || x.Proveedor.ToLower().Contains(parametro.ToLower())
                    || x.Monto.ToString() == parametro)).OrderByDescending(x => x.Id).ToList());
            }

        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            FillDropdownLists();

            return View("Edit", dbContext.RegistroSolicitudCheques.Find(id));
        }

        [HttpPost]
        public IActionResult Add(RegistroSolicitudCheque registroSolicitudCheque)
        {
            //nomenclatura de la CuentaContableRelacionada --> nombre de la cuenta + aplicativo + a;o y mes + 001 
            var ccproveedor = dbContext.Proveedores.FirstOrDefault(x => x.Nombre == registroSolicitudCheque.Proveedor);

            if (ccproveedor != null)
            {
                Random r = new Random();
                registroSolicitudCheque.NumeroSolicitud = "SO-" + r.Next(1000, 1000000).ToString();
                registroSolicitudCheque.Estado = "Pendiente";
                registroSolicitudCheque.CuentaContableProveedor = ccproveedor.CuentaContableProveedor;
                registroSolicitudCheque.CuentaContableRelacionada = "CCR-AC202311000" + r.Next(1, 100).ToString();

                if (registroSolicitudCheque.FechaRegistroString != "1/1/0001")
                {
                    registroSolicitudCheque.FechaRegistro = registroSolicitudCheque.FechaRegistro;
                }
                else
                {
                    registroSolicitudCheque.FechaRegistro = DateTime.Now;
                }

                dbContext.Add(registroSolicitudCheque);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["err"] = "Debe seleccionar un Proveedor";
                return RedirectToAction("Add");
            }

        }

        [HttpPost]
        [Route("RegistroSolicitudCheque/Edit")]
        public IActionResult Edit(RegistroSolicitudCheque registroSolicitudCheque)
        {
            //nomenclatura de la CuentaContableRelacionada --> nombre de la cuenta + aplicativo + a;o y mes + 001
            var ccproveedor = dbContext.Proveedores.FirstOrDefault(x => x.Nombre == registroSolicitudCheque.Proveedor);

            if (ccproveedor != null)
            {
                Random r = new Random();
                registroSolicitudCheque.NumeroSolicitud = "SO-" + r.Next(1000, 1000000).ToString();
                registroSolicitudCheque.Estado = "Pendiente";
                registroSolicitudCheque.CuentaContableProveedor = ccproveedor.CuentaContableProveedor;
                registroSolicitudCheque.CuentaContableRelacionada = "CCR-AC2023110001";
                dbContext.Update(registroSolicitudCheque);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["err"] = "Debe seleccionar un Proveedor";
                return View("Edit");
            }
            
            
        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/UpdateC")]
        public IActionResult UpdateC(int id)
        {
            Random r = new Random();
            var registroSolicitudCheque = dbContext.RegistroSolicitudCheques.FirstOrDefault(x => x.Id == id);
            var nuevoEstado = "Cheque Generado";
            var nuevoNoCheque = "CH-000" + r.Next(1, 1000).ToString();
            if (registroSolicitudCheque != null)
            {
                registroSolicitudCheque.NumeroSolicitud = nuevoNoCheque;
                registroSolicitudCheque.Estado = nuevoEstado;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/UpdateA")]
        public IActionResult UpdateA(int id)
        {
            var registroSolicitudCheque = dbContext.RegistroSolicitudCheques.FirstOrDefault(x => x.Id == id);
            var nuevoEstado = "Solicitud Anulada";
            if (registroSolicitudCheque != null)
            {
                registroSolicitudCheque.Estado = nuevoEstado;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("RegistroSolicitudCheque/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.RegistroSolicitudCheques.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private void FillDropdownLists()
        {
            ViewBag.Proveedor = new SelectList(dbContext.Proveedores.OrderByDescending(x => x.Id).ToList(), "Nombre", "Nombre");
        }
    }
}
