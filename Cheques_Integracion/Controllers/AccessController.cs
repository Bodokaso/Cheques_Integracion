using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Cheques_Integracion.Models; // Replace with the correct namespace for your User model
using Microsoft.EntityFrameworkCore;

namespace Cheques_Integracion.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;

        public AccessController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario modelLogin)
        {
            // Query the database to check if the user with the provided email and password exists
            var user = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.Correo == modelLogin.Correo && u.Clave == modelLogin.Clave);

            if (user != null)
            {
                // User found, create claims and sign in
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Correo),
                    new Claim(ClaimTypes.Name, user.NombreCompleto), // Include other user-related claims here
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found or invalid credentials.";
            return View();
        }
    }
}
