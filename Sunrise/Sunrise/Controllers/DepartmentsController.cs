using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunrise.Data;
using Sunrise.Models;

namespace Sunrise.Controllers
{
    public class DepartmentsController : Controller
    {
        ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]   
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Departments.ToList());
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Department department = _context.Departments.Include(d=>d.Employees).FirstOrDefault(e => e.Id == id);

            return View("Details", department);
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Department emp) // FolanAlfolani.png
        {
            if (ModelState.IsValid == true)
            {
                _context.Departments.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Create");
            }
        }


        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Department department = _context.Departments.FirstOrDefault(e => e.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", department);
            }
        }


        [HttpPost]
        public IActionResult EditCurrent(Department emp)
        {
            if (ModelState.IsValid == true)
            {
                _context.Departments.Update(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit");
            }
        }


        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Department department = _context.Departments.Include(d => d.Employees).FirstOrDefault(e => e.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", department);
            }
        }


        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Department department = _context.Departments.FirstOrDefault(e => e.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }

    }
}
