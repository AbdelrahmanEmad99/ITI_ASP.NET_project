using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunrise.Data;
using Sunrise.Models;

namespace Sunrise.Controllers
{
    public class EmployeesController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Employees.ToList());
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Employee employee = _context.Employees.Include(e=>e.Department).FirstOrDefault(e => e.Id == id);

            return View("Details", employee);
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Employee emp, IFormFile? imageFormFile) // FolanAlfolani.png
        {
            // GUID -> Globally Unique Identifier
            if (imageFormFile != null)
            {
                string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
                string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
                string imgUrl = "\\images\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
                emp.ImageUrl = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

                // FileStream 
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();
            }
            else
            {
                emp.ImageUrl = "\\images\\No_Image.png";
            }

            if (((emp.JoinDate - emp.BirthDate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Illegal Hiring Age (Under 18 years old).");
            }

            if(emp.Salary > 55000 || emp.Salary < 5500)
            {
                ModelState.AddModelError(string.Empty, "Salary mustn't be less than 5500 EGP or greater than 55000 EGP.");
            }

            if (ModelState.IsValid == true)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
                return View("Create");
            }
        }


        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
                return View("Edit", employee);
            }
        }


        [HttpPost]
        public IActionResult EditCurrent(Employee emp, IFormFile? imageFormFile)
        {
            // GUID -> Globally Unique Identifier
            if (imageFormFile != null)
            {
                if (emp.ImageUrl != "\\images\\No_Image.png")
                {
                    string oldImgPath = _webHostEnvironment.WebRootPath + emp.ImageUrl;

                    if (System.IO.File.Exists(oldImgPath) == true)
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }


                string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
                string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
                string imgUrl = "\\images\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
                emp.ImageUrl = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

                // FileStream 
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();
            }




            if (((emp.JoinDate - emp.BirthDate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Illegal Hiring Age (Under 18 years old).");
            }

            if (emp.Salary > 55000 || emp.Salary < 5500)
            {
                ModelState.AddModelError(string.Empty, "Salary mustn't be less than 5500 EGP or greater than 55000 EGP.");
            }

            if (ModelState.IsValid == true)
            {
                _context.Employees.Update(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
                return View("Edit");
            }
        }


        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Employee employee = _context.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", employee);
            }
        }


        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                if (employee.ImageUrl != "\\images\\No_Image.png")
                {
                    string imgPath = _webHostEnvironment.WebRootPath + employee.ImageUrl;

                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }
                

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }



















        public string GreetVisitor()
        {
            return "Welcome to Sunrise!";
        }

        public string GreetUser(string name)
        {
            return $"Hi {name}\nHow are you?";
        }

        public string GetAge(string name, int birthYear)
        {
            return $"Hi {name}\nYou are {DateTime.Now.Year - birthYear} years old.";
        }
    }
}
