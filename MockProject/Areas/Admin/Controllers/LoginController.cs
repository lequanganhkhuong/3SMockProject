using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockProject.Models;

namespace MockProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "username or password can't be empty";
                return RedirectToAction(nameof(Index));
            }

            var emp = _context.Users
                .Where(x => x.Username.Equals(username) && x.Password.Equals(password)).Include(x=>x.Role).FirstOrDefault();
            if (emp == null)
            {
                ViewBag.Message = "Wrong username or password";
                return RedirectToAction(nameof(Index));
            }
            else if(!emp.IsActive)
            {
                ViewBag.Message = "This user is blocked";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,emp.Id.ToString()),
                    new Claim("Username", emp.Name), 
                    new Claim(ClaimTypes.Role,emp.Role.Name), 
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal);
            
                return RedirectToAction("Index","Home");
            }
            
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult ErrorForbbiden() => View();
    }
}