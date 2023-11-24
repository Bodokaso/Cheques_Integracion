using Cheques_Integracion.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cheques_Integracion.Controllers
{
    public class LoginController : Controller
    {

        private readonly AppDbContext dbContext;
        public LoginController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        [Route("Login/Registro")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            dbContext.Add(usuario);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
           
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(Usuario modelLogin)
        {
            // Query the database to check if the user with the provided email and password exists
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u =>
                u.Correo == modelLogin.Correo && u.Clave == modelLogin.Clave);

            if (user != null)
            {
                // User found, create claims and sign in
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Correo),
                    new Claim(ClaimTypes.Name, user.NombreCompleto), // Include other user-related claims here
                };
                                
                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "Credenciales Invalidas";
            return View("Index");
        }
    }
}
