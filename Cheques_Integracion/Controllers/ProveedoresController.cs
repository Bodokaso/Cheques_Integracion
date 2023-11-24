using Cheques_Integracion.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheques_Integracion.Controllers
{
    public class ProveedoresController : Controller
    {

        private readonly AppDbContext dbContext;
        public ProveedoresController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(dbContext.Proveedores.OrderByDescending(x => x.Id).ToList());
        }

        [HttpGet]
        [Route("Proveedores/Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Route("Proveedores/{parametro}")]
        public IActionResult Search(string parametro = null)
        {
            if (parametro == "Search")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View("Index", dbContext.Proveedores.Where(x => (x.Id.ToString() == parametro
                    || x.NumeroIdentificacion == parametro
                    || x.Nombre.ToLower().Contains(parametro.ToLower())
                    || x.TipoPersona.ToLower().Contains(parametro.ToLower())
                    || x.Balance.ToString() == parametro
                    || x.CuentaContableProveedor == parametro
                    || (x.Estado && (parametro.ToLower() == "activo"))
                    || (!x.Estado && (parametro.ToLower() == "inactivo")))).OrderByDescending(x => x.Id).ToList());
            }

        }

        [HttpGet]
        [Route("Proveedores/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            return View("Edit", dbContext.Proveedores.Find(id));
        }

        [HttpPost]
        public IActionResult Add(Proveedore provider)
        {
            if (validaCedula(provider.NumeroIdentificacion) == true && provider.Balance > 0)
            {
                var ccproveedor = "CCP-";
                provider.CuentaContableProveedor = ccproveedor + provider.CuentaContableProveedor;

                dbContext.Add(provider);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                TempData["err"] = "Revisar campos";
                return RedirectToAction("Add");
            }

        }

        [HttpPost]
        [Route("Proveedores/Edit")]
        public IActionResult Edit(Proveedore provider)
        {
            int id = provider.Id;

            if (validaCedula(provider.NumeroIdentificacion) == true && provider.Balance > 0)
            {
                var ccproveedor = "CCP-";
                provider.CuentaContableProveedor = ccproveedor + provider.CuentaContableProveedor;
                dbContext.Update(provider);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                TempData["err"] = "Revisar campos";
                return View("Edit", dbContext.Proveedores.Find(id));
            }
        }

        [HttpGet]
        [Route("Proveedores/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.Proveedores.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public static bool validaCedula(string pCedula)

        {
            try
            {
                int vnTotal = 0;
                string vcCedula = pCedula.Replace("-", "");
                int pLongCed = vcCedula.Trim().Length;
                int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
                if (pLongCed < 11 || pLongCed > 11)
                    return false;
                for (int vDig = 1; vDig <= pLongCed; vDig++)
                {
                    int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                    if (vCalculo < 10)
                        vnTotal += vCalculo;
                    else
                        vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
                }
                if (vnTotal % 10 == 0)
                    return true;
                else
                    return false;
            }

            catch
            {
                return false;
            }

        }
    }
}
