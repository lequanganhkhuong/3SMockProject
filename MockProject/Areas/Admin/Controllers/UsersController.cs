using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MockProject.Models;

namespace MockProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string search, string filter, string sort)
        {
            ViewBag.Pages = "User";
            ViewBag.Name = string.IsNullOrEmpty(sort) ? "Name_desc" : "";
            ViewBag.Gender = sort == "male" ? "female" : "male";
            ViewBag.IsActive = sort == "true" ? "false" : "true";
            ViewBag.search = search;
            ViewBag.Filter = filter ?? "0";
            
            IQueryable<User> users = _context.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                int role = int.Parse(filter);
                if (role != 1 || role != 2 || role != 3 || role != 0)
                {
                    return Content("CLGT??");
                }
                if (role != 0)
                {
                    users = users.Where(x => x.RoleId == role);
                   
                }
                
            }
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => x.Name.Contains(search) || x.Username.Contains(search));
            }
            switch (sort)
            {
                    
                case "Name_desc":
                    users = users.OrderByDescending(x => x.Name);
                    break;
                case "male":
                    users = users.OrderBy(x => x.Gender);
                    break;
                case "female":
                    users = users.OrderByDescending(x => x.Gender);
                    break;
                case "true":
                    users = users.OrderBy(x => x.IsActive);
                    break;
                case "false":
                    users = users.OrderByDescending(x => x.IsActive);
                    break;
                default:
                    users = users.OrderBy(x => x.Name);
                    break;
            }
            return View(await users.Include(u => u.Faculty).Include(u => u.Role).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Faculty)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.Pages = "User";
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Code,Name,Birthday,Address,Gender,IsActive,IsGraduated,FacultyId,RoleId")] User user)
        {
            ViewBag.Pages = "User";
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Code,Name,Birthday,Address,Gender,IsActive,IsGraduated,FacultyId,RoleId")] User user)
        {
            ViewBag.Pages = "User";
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Faculty)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Pages = "User";
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
