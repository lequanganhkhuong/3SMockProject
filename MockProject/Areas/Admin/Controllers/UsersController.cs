using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            
            IQueryable<User> users = _unitOfWork.UserRepository.GetAll();
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
        public  IActionResult Details(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user =  _unitOfWork.UserRepository.Get(id);
           
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
            ViewData["FacultyId"] = new SelectList(_unitOfWork.FacultyRepository.GetAll().ToList(), "Id", "Id");
            ViewData["RoleId"] = new SelectList(_unitOfWork.RoleRepository.GetAll().ToList(), "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Username,Password,Code,Name,Birthday,Address,Gender,IsActive,IsGraduated,FacultyId,RoleId")] User user)
        {
            ViewBag.Pages = "User";
            if (ModelState.IsValid)
            {
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_unitOfWork.FacultyRepository.GetAll().ToList(), "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_unitOfWork.RoleRepository.GetAll().ToList(), "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public  IActionResult Edit(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user = _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_unitOfWork.FacultyRepository.GetAll().ToList(), "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_unitOfWork.RoleRepository.GetAll().ToList(), "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Username,Password,Code,Name,Birthday,Address,Gender,IsActive,IsGraduated,FacultyId,RoleId")] User user)
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
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_unitOfWork.FacultyRepository.GetAll().ToList(), "Id", "Id", user.FacultyId);
            ViewData["RoleId"] = new SelectList(_unitOfWork.RoleRepository.GetAll().ToList(), "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int? id)
        {
            ViewBag.Pages = "User";
            if (id == null)
            {
                return NotFound();
            }

            var user = _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewBag.Pages = "User";
            var user = _unitOfWork.UserRepository.Get(id);
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
